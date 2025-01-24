using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardGoldenWaveActivation : MonoBehaviour, IPointerClickHandler
{
    public GameObject CardHolder;
    // Start is called before the first frame update
    void Start()
    {

    }


    public void OnPointerClick(PointerEventData eventData)
    {

        foreach (var comp in CardHolder.GetComponents<Component>())
        {
            if (!(comp is Transform))
            {
                Destroy(comp);
            }
        }
        CardHolder.AddComponent(typeof(CardGoldenWave));
    }
}
