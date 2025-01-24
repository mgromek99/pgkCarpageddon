using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGoldenWave : MonoBehaviour
{
    private SpawnEnemy spawnEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        spawnEnemy = FindObjectOfType<SpawnEnemy>();

        spawnEnemy.coinMult = 1.5f;

        spawnEnemy.healthMult = 1.3f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        spawnEnemy.coinMult = 1.0f;

        spawnEnemy.healthMult = 1.0f;
    }
}
