using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyType
    {
        public GameObject prefab; // Prefab przeciwnika
        public int weight; // Waga losowania (im wy�sza, tym wi�ksza szansa na spawn)
    }

    public EnemyType[] enemyTypes; // Tablica r�nych rodzaj�w przeciwnik�w

    public int minEnemiesPerWave = 3; // Minimalna liczba przeciwnik�w na fal�
    public int maxEnemiesPerWave = 10; // Maksymalna liczba przeciwnik�w na fal�
    public float minSpawnDelay = 1.5f; // Minimalny czas mi�dzy spawnami
    public float maxSpawnDelay = 4.0f; // Maksymalny czas mi�dzy spawnami
    public float waveDelay = 1f; // Czas mi�dzy falami

    private int waveCounter= 0;
    public bool spawning = false; // Czy spawner jest aktywny
    private float timeToNextWave; // Czas do rozpocz�cia nast�pnej fali
    private float timeToNextSpawn; // Czas do nast�pnego spawnu w bie��cej fali
    public int enemiesInWave=0; // Liczba przeciwnik�w w bie��cej fali
    public int enemiesSpawned=0; // Liczba ju� stworzonych przeciwnik�w w bie��cej fali

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

        // Je�li czas na rozpocz�cie nowej fali
        if (enemiesSpawned<=enemiesInWave)
        {
            // Je�li nie ma przeciwnik�w w bie��cej fali, rozpocznij now� fal�
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

                    // Losowy czas na nast�pny spawn
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
            timeToNextWave -= Time.deltaTime; // Odliczaj czas do nast�pnej fali
        }
    }

    private void StartNewWave()
    {
        // Losowa liczba przeciwnik�w w nowej fali
        enemiesInWave = Random.Range(minEnemiesPerWave, maxEnemiesPerWave + 1);
        enemiesSpawned = 0;
        Debug.Log($"Spawning wave with {enemiesInWave} enemies.");
        timeToNextWave = waveDelay; // Ustaw czas na kolejn� fal�
    }

    private int GetRandomEnemyType()
    {
        int totalWeight = 0;

        // Oblicz sum� wag wszystkich przeciwnik�w
        foreach (var enemy in enemyTypes)
        {
            totalWeight += enemy.weight;
        }

        // Losuj warto�� w zakresie sumy wag
        int randomWeight = Random.Range(0, totalWeight);
        int currentWeight = 0;

        // Znajd� przeciwnika na podstawie wylosowanej warto�ci
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            currentWeight += enemyTypes[i].weight;
            if (randomWeight < currentWeight)
            {
                return i;
            }
        }

        return 0; // W domy�le zwr�� pierwszy typ
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