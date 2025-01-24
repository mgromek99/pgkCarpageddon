using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRandomSpeed : MonoBehaviour
{
    private SpawnEnemy spawnEnemy;
    private float randomMult;

    private void OnEnable()
    {
        randomMult = Random.Range(0.7f, 1.15f);
        // Find the GameMaster component in the scene
        spawnEnemy = FindObjectOfType<SpawnEnemy>();

        spawnEnemy.speedMult= randomMult;
    }

    private void OnDestroy()
    {
        spawnEnemy.speedMult = 1.0f;
    }
}
