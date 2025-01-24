using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotation : MonoBehaviour
{
    public Camera kamera;

    void Start()
    {
        if (kamera == null)
        {
            kamera = Camera.main;
        }
    }
    void Update()
    {
        transform.rotation = kamera.transform.rotation;
        
    }

}
