using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelectDalej : MonoBehaviour, IPointerClickHandler
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public EnemyWaveSpawner EWS;
    // Start is called before the first frame update

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Card clicked!");
        EWS.spawning = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
