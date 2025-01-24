using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRemoveElement : MonoBehaviour
{

    private EnemyWaveSpawner EWS;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        EWS = FindObjectOfType<EnemyWaveSpawner>();
        int blockedType = Random.Range(0, 2);
        EWS.BlockType(blockedType);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        EWS.UnblockType();
    }
}
