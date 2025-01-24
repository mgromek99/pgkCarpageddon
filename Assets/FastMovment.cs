using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FastMovment : MonoBehaviour
{

    
    public float speed = 2;
   private Vector3 meta = new Vector3(-1f, 0.25f, -4f);
   private Vector3 start = new Vector3(3f, 0.25f, 2f);
    // bool flag = true;
    private List<Tiles> road; 
    
    private bool didWeWalking;
   
    private int whereWeAt;
    // Start is called before the first frame update


   
    void Start()
    {
    
        
    }

    public void MoveYourFatAss(List<Tiles> path)
    {
        road = path;
        whereWeAt = 0;
        didWeWalking = true;
       
        

    }
    public void MoveUpdate()
    {
        start = road[whereWeAt].Position;
        start.y += 0.25f;
        meta = road[whereWeAt + 1].Position;
        meta.y += 0.25f;
       // Debug.Log($"Pozycja startowa {start} Pozycja ko�cowa {meta}");
    }
    // Update is called once per frame
    void Update()
    {

        if (didWeWalking)
        {
          
            MoveUpdate();

            if (meta == transform.position)
            {
                if (whereWeAt + 2 >= road.Count)
                {
                    didWeWalking = false;
                    DespawnCube();
                    //Debug.Log($"dlaczego");
                }
                else
                {
                    whereWeAt += 1;
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, meta, speed * Time.deltaTime);

        }


    }
   
    private void DespawnCube()
    {
        GameMaster.Instance.LostHealthPoint();
            Destroy(gameObject);
            
        
    }
}
