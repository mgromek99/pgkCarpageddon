using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyType
    {
        public GameObject prefab; // Prefab przeciwnika
        public int weight; // Waga losowania (im wy¿sza, tym wiêksza szansa na spawn)
    }

    public EnemyType[] enemyTypes; // Tablica ró¿nych rodzajów przeciwników

    public int minEnemiesPerWave = 3; // Minimalna liczba przeciwników na falê
    public int maxEnemiesPerWave = 10; // Maksymalna liczba przeciwników na falê
    public float minSpawnDelay = 1.5f; // Minimalny czas miêdzy spawnami
    public float maxSpawnDelay = 4.0f; // Maksymalny czas miêdzy spawnami
    public float waveDelay = 1f; // Czas miêdzy falami

    private int waveCounter= 0;
    public bool spawning = false; // Czy spawner jest aktywny
    private float timeToNextWave; // Czas do rozpoczêcia nastêpnej fali
    private float timeToNextSpawn; // Czas do nastêpnego spawnu w bie¿¹cej fali
    public int enemiesInWave=0; // Liczba przeciwników w bie¿¹cej fali
    public int enemiesSpawned=0; // Liczba ju¿ stworzonych przeciwników w bie¿¹cej fali

    private int blockedType;
    private int blockedTypeWeight;

    public GameObject panel;

    private void Start()
    {
        // Ustaw czas do pierwszej fali
        timeToNextWave = -1f;
    }

    private void Update()
    {
        if (enemiesSpawned >= enemiesInWave && spawning && !panel.active)
        {
            Debug.Log("chbuj");
            spawning = false;
            panel.SetActive(true);
        }
           


        if (!spawning)
            return;

        // Jeœli czas na rozpoczêcie nowej fali
        if (enemiesSpawned<=enemiesInWave)
        {
            // Jeœli nie ma przeciwników w bie¿¹cej fali, rozpocznij now¹ falê
            if (enemiesSpawned >= enemiesInWave)
            {
                StartNewWave();
                panel.SetActive(false);
                waveCounter += 1;
                minSpawnDelay -= 0.1f;
                maxEnemiesPerWave += 2;
            }
            else
            {
                // Spawnowanie przeciwnika
                if (timeToNextSpawn <= 0.1f)
                {
                    int numerek = GetRandomEnemyType();
                    GameMaster.Instance.SpawnEnemy(numerek);
                    enemiesSpawned++;

                    // Losowy czas na nastêpny spawn
                    timeToNextSpawn = Random.Range(minSpawnDelay, maxSpawnDelay);
                }
                else
                {
                    timeToNextSpawn -= Time.deltaTime;
                }
            }
        }
        else
        {
            timeToNextWave -= Time.deltaTime; // Odliczaj czas do nastêpnej fali
        }
    }

    private void StartNewWave()
    {
        // Losowa liczba przeciwników w nowej fali
        enemiesInWave = Random.Range(minEnemiesPerWave, maxEnemiesPerWave + 1);
        enemiesSpawned = 0;
        Debug.Log($"Spawning wave with {enemiesInWave} enemies.");
        timeToNextWave = waveDelay; // Ustaw czas na kolejn¹ falê
    }

    private int GetRandomEnemyType()
    {
        int totalWeight = 0;

        // Oblicz sumê wag wszystkich przeciwników
        foreach (var enemy in enemyTypes)
        {
            totalWeight += enemy.weight;
        }

        // Losuj wartoœæ w zakresie sumy wag
        int randomWeight = Random.Range(0, totalWeight);
        int currentWeight = 0;

        // ZnajdŸ przeciwnika na podstawie wylosowanej wartoœci
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            currentWeight += enemyTypes[i].weight;
            if (randomWeight < currentWeight)
            {
                return i;
            }
        }

        return 0; // W domyœle zwróæ pierwszy typ
    }

    public void StopSpawning()
    {
        spawning = false; // Zatrzymaj spawn
    }

    public void BlockType(int type)
    {
        blockedType = type;
        blockedTypeWeight = enemyTypes[blockedType].weight;
        enemyTypes[blockedType].weight = 0;
    }

    public void UnblockType()
    {
        enemyTypes[blockedType].weight = blockedTypeWeight;
    }
}