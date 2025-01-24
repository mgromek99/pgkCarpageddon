using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnEnemy : MonoBehaviour
{
    

    private List<Tiles> path;
   // private GameObject spawnedCube;
  //  private FastMovment moveScript;
   private Vector3 meta = new Vector3(-1f, 0.25f, -4f);
    private Vector3 start = new Vector3(3f, 0.25f, 2f);
    public float speedMult = 1.0f;
    public float healthMult = 1.0f;
    public float coinMult = 1.0f;

    void Awake()
    {
      
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    public void PrepareSpawn(GameObject theEnemy)
    {
       
        path = GameMaster.Instance.map.path1;
            start = path[1].Position;
            meta = path[path.Count-1].Position;
         GameObject obj = SpawnCube(start,theEnemy);
        FastMovment moveScript = obj.GetComponent<FastMovment>();
        moveScript.MoveYourFatAss(GameMaster.Instance.map.path1);

            // moveBitch = gameObject.AddComponent<FastMovment>();
            // moveBitch.MoveYourFatAss(path1);
      
    }
    private GameObject SpawnCube(Vector3 pos, GameObject theEnemy)
    {
        
            pos.y += 0.25f;
        //spawnedCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
       GameObject spawnedCube = Instantiate(theEnemy,pos,Quaternion.identity);
        
        spawnedCube.transform.position = pos;
            spawnedCube.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            spawnedCube.AddComponent<FastMovment>();
        spawnedCube.GetComponent<FastMovment>().speed *= speedMult;
            spawnedCube.AddComponent<HealthSystem>();
        HealthSystem cubeHS = spawnedCube.GetComponent<HealthSystem>();
        cubeHS.maxHealth = Mathf.RoundToInt(cubeHS.maxHealth*healthMult) ;
        cubeHS.coinAmount = Mathf.RoundToInt(cubeHS.coinAmount * coinMult);
        spawnedCube.AddComponent<SpriteRotation>();
        
           // moveScript = spawnedCube.GetComponent<FastMovment>();
        return spawnedCube;
        
    }
   /* private void DespawnCube()
    {
        if (spawnedCube != null)
        {
            Destroy(spawnedCube);
            spawnedCube = null;
        }
    }
   */
}
