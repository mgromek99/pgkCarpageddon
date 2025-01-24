using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;

    
    public static Tiles CreateTile(GameObject parent, Vector3 position, string tileType, bool isWalkable)
    {
        
        GameObject tileObject = new GameObject("Tile");




        
        Tiles tile = tileObject.AddComponent<Tiles>();

      
        tile.Init(position, tileType, isWalkable);

        GameObject tilePrefab = Resources.Load<GameObject>(tileType);

        if (tilePrefab != null)
        {
            
            GameObject prefabInstance = Instantiate(tilePrefab, position, Quaternion.identity);
            prefabInstance.transform.SetParent(tileObject.transform);
            prefabInstance.transform.localPosition = Vector3.zero; 
        }
        else
        {
            Debug.LogError("Prefab nie zosta³ znaleziony!");
        }

       
        tileObject.transform.SetParent(parent.transform);
        tileObject.transform.position = position;

        return tile;
    }

    void Awake()
    {       
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    /* public static Tiles fastTileCreator(Vector3 position)
     {
             Tiles tile = new Tiles(position, "Grass", false);
             return tile;   
     }*/
}