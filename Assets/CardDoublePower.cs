using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDoublePower : MonoBehaviour
{
    private GameMaster gm;
    private float powerMult = 2.0f;
    List<int> originalDamage = new List<int>();
    private SpawnEnemy spawnEnemy;
    //AimAndShootAtEnemy[] turrets;
    // Start is called before the first frame update
    void Start()
    {

    }


    private void Awake()
    {
        gm = FindObjectOfType<GameMaster>();
        spawnEnemy = FindObjectOfType<SpawnEnemy>();
        gm.healthLost = 2;
        spawnEnemy.healthMult = 0.5f;
        //turrets = GetComponents<AimAndShootAtEnemy>();

        /*for (int i = 0; i < turrets.Length; i++)
        {
            originalDamage.Add(turrets[i].damageAmount);
            turrets[i].damageAmount =Mathf.CeilToInt(turrets[i].damageAmount*powerMult);
            Debug.Log(turrets[i].damageAmount);
        }*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        gm.healthLost = 1;
        //for (int i = 0; i < turrets.Length; i++)
        //{
        //    turrets[i].damageAmount = originalDamage[i];
        //}
        spawnEnemy.healthMult = 1.0f;
    }

}
