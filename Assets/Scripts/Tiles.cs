using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public Vector3 Position { get; private set; } 
    public string TileType { get; private set; }  
    public bool IsWalkable { get; private set; }  

    [HideInInspector] public List<Tiles> neighbours;

    private GameObject tilePrefabInstance; 

    
    public void Init(Vector3 position, string tileType, bool isWalkable)
    {
        this.Position = position;
        this.TileType = tileType;
        this.IsWalkable = isWalkable;
        this.neighbours = new List<Tiles>();
       
        LoadPrefab(tileType);
    }

 
    private void LoadPrefab(string tileType)
    {
        GameObject prefab = Resources.Load<GameObject>(tileType);

        if (prefab != null)
        {
            tilePrefabInstance = Instantiate(prefab, transform.position, Quaternion.identity, transform);
            tilePrefabInstance.transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.LogError("Prefab dla typu " + tileType + " nie zosta³ znaleziony.");
        }
    }

    
    public void ChangeTileType(string newTileType)
    {
        
        if (tilePrefabInstance != null)
        {
            Destroy(tilePrefabInstance); 
            tilePrefabInstance = null;
        }

        
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        this.TileType = newTileType;     
        LoadPrefab(newTileType);
    }
}