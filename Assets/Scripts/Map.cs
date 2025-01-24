using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    List<Tiles> hexes = new List<Tiles>();
    public GameObject prefab;
    public int mapLayers;
    public static Map debugTile;
    public Vector3 spawnposition;
    public GameObject map;
    [HideInInspector]
    public List<Tiles> path1;

    private Pathfinding pathfinding;
    private Tiles startTile;
    private Tiles endTile;
   

    // Start is called before the first frame update
    public void GenerateMap()
    {

        Vector3 positionHolder = new Vector3(0, 0, 0);
        Vector3 secoundPositionHolder = new Vector3(0, 0, 0);
        Tiles newTile;
        bool copyCheck = false;

        if (prefab == null)
        {
            Debug.LogError("Prefab nie jest ustawiony.");
            return;
        }

        newTile = TileManager.CreateTile(map, positionHolder, "grass", false);
        hexes.Add(newTile);

        for (int i = 1; i < mapLayers; i++)
        {


            positionHolder = spawnposition;
            positionHolder.z = positionHolder.z + Mathf.Sqrt(3) / 2 * i;
            //Instantiate(prefab, positionHolder, Quaternion.identity);
            newTile = TileManager.CreateTile(map, positionHolder, "grass", false);
            foreach (var hexe in hexes)
            {
                if (hexe.Position == positionHolder)
                {
                    copyCheck = true; break;
                }
            }
            if (copyCheck == false)
            {
                hexes.Add(newTile);
            }
            copyCheck = false;

            positionHolder = spawnposition;
            positionHolder.z = positionHolder.z - Mathf.Sqrt(3) / 2 * i;
            // Instantiate(prefab, positionHolder, Quaternion.identity);
            // newTile = TileManager.fastTileCreator(positionHolder);
            newTile = TileManager.CreateTile(map, positionHolder, "Grass", false);
            foreach (var hexe in hexes)
            {
                if (hexe.Position == positionHolder)
                {
                    copyCheck = true; break;
                }
            }
            if (copyCheck == false)
            {
                hexes.Add(newTile);
            }
            copyCheck = false;

            for (int j = 1; j + i < mapLayers; j++)
            {
                secoundPositionHolder = positionHolder;
                secoundPositionHolder.z = positionHolder.z - Mathf.Sqrt(3) / 4 * j;
                secoundPositionHolder.x = secoundPositionHolder.x - 1.5f / 2 * j;
                //  Instantiate(prefab, secoundPositionHolder, Quaternion.identity);
                newTile = TileManager.CreateTile(map, secoundPositionHolder, "Grass", false);
                foreach (var hexe in hexes)
                {
                    if (hexe.Position == secoundPositionHolder)
                    {
                        copyCheck = true; break;
                    }
                }
                if (copyCheck == false)
                {
                    hexes.Add(newTile);
                }
                copyCheck = false;

                secoundPositionHolder = positionHolder;
                secoundPositionHolder.z = positionHolder.z - Mathf.Sqrt(3) / 4 * j;
                secoundPositionHolder.x = secoundPositionHolder.x + 1.5f / 2 * j;
                //  Instantiate(prefab, secoundPositionHolder, Quaternion.identity);
                newTile = TileManager.CreateTile(map, secoundPositionHolder, "Grass", false);
                foreach (var hexe in hexes)
                {
                    if (hexe.Position == secoundPositionHolder)
                    {
                        copyCheck = true; break;
                    }
                }
                if (copyCheck == false)
                {
                    hexes.Add(newTile);
                }
                copyCheck = false;
            }

            positionHolder = spawnposition;
            positionHolder.z = positionHolder.z + Mathf.Sqrt(3) / 4 * i;
            positionHolder.x = positionHolder.x + 1.5f / 2 * i;
            //  Instantiate(prefab, positionHolder, Quaternion.identity);
            newTile = TileManager.CreateTile(map, positionHolder, "Grass", false);
            foreach (var hexe in hexes)
            {
                if (hexe.Position == positionHolder)
                {
                    copyCheck = true; break;
                }
            }
            if (copyCheck == false)
            {
                hexes.Add(newTile);
            }
            copyCheck = false;

            for (int j = 1; j + i < mapLayers; j++)
            {
                secoundPositionHolder = positionHolder;
                secoundPositionHolder.z = secoundPositionHolder.z + Mathf.Sqrt(3) / 2 * j;
                //     Instantiate(prefab, secoundPositionHolder, Quaternion.identity);
                newTile = TileManager.CreateTile(map, secoundPositionHolder, "Grass", false);
                foreach (var hexe in hexes)
                {
                    if (hexe.Position == secoundPositionHolder)
                    {
                        copyCheck = true; break;
                    }
                }
                if (copyCheck == false)
                {
                    hexes.Add(newTile);
                }
                copyCheck = false;

                secoundPositionHolder.z = positionHolder.z - Mathf.Sqrt(3) / 4 * j;
                secoundPositionHolder.x = secoundPositionHolder.x + 1.5f / 2 * j;
                //     Instantiate(prefab, secoundPositionHolder, Quaternion.identity);
                newTile = TileManager.CreateTile(map, secoundPositionHolder, "Grass", false);
                foreach (var hexe in hexes)
                {
                    if (hexe.Position == secoundPositionHolder)
                    {
                        copyCheck = true; break;
                    }
                }
                if (copyCheck == false)
                {
                    hexes.Add(newTile);
                }
                copyCheck = false;

            }


            positionHolder = spawnposition;
            positionHolder.z = positionHolder.z - Mathf.Sqrt(3) / 4 * i;
            positionHolder.x = positionHolder.x + 1.5f / 2 * i;
            //  Instantiate(prefab, positionHolder, Quaternion.identity);
            newTile = TileManager.CreateTile(map, positionHolder, "Grass", false);
            foreach (var hexe in hexes)
            {
                if (hexe.Position == positionHolder)
                {
                    copyCheck = true; break;
                }
            }
            if (copyCheck == false)
            {
                hexes.Add(newTile);
            }
            copyCheck = false;
            positionHolder = spawnposition;
            positionHolder.z = positionHolder.z + Mathf.Sqrt(3) / 4 * i;
            positionHolder.x = positionHolder.x - 1.5f / 2 * i;
            //   Instantiate(prefab, positionHolder, Quaternion.identity);
            newTile = TileManager.CreateTile(map, positionHolder, "Grass", false);
            foreach (var hexe in hexes)
            {
                if (hexe.Position == positionHolder)
                {
                    copyCheck = true; break;
                }
            }
            if (copyCheck == false)
            {
                hexes.Add(newTile);
            }
            copyCheck = false;


            for (int j = 1; j + i < mapLayers; j++)
            {
                secoundPositionHolder = positionHolder;
                secoundPositionHolder.z = secoundPositionHolder.z + Mathf.Sqrt(3) / 2 * j;
                //     Instantiate(prefab, secoundPositionHolder, Quaternion.identity);
                newTile = TileManager.CreateTile(map, secoundPositionHolder, "Grass", false);
                foreach (var hexe in hexes)
                {
                    if (hexe.Position == secoundPositionHolder)
                    {
                        copyCheck = true; break;
                    }
                }
                if (copyCheck == false)
                {
                    hexes.Add(newTile);
                }
                copyCheck = false;

                secoundPositionHolder.z = positionHolder.z - Mathf.Sqrt(3) / 4 * j;
                secoundPositionHolder.x = secoundPositionHolder.x - 1.5f / 2 * j;
                //     Instantiate(prefab, secoundPositionHolder, Quaternion.identity);
                newTile = TileManager.CreateTile(map, secoundPositionHolder, "Grass", false);
                foreach (var hexe in hexes)
                {
                    if (hexe.Position == secoundPositionHolder)
                    {
                        copyCheck = true; break;
                    }
                }
                if (copyCheck == false)
                {
                    hexes.Add(newTile);
                }
                copyCheck = false;
            }

            positionHolder = spawnposition;
            positionHolder.z = positionHolder.z - Mathf.Sqrt(3) / 4 * i;
            positionHolder.x = positionHolder.x - 1.5f / 2 * i;
            //   Instantiate(prefab, positionHolder, Quaternion.identity);
            newTile = TileManager.CreateTile(map, positionHolder, "Grass", false);
            foreach (var hexe in hexes)
            {
                if (hexe.Position == positionHolder)
                {
                    copyCheck = true; break;
                }
            }
            if (copyCheck == false)
            {
                hexes.Add(newTile);
            }
            copyCheck = false;

        }
        hexes = getNeighbours(hexes);
        pathfinding = new Pathfinding(hexes);

        int sizeMap = (mapLayers - 1) * 4;

        pathfinding.SetStartAndEndTiles(pathfinding.FindStartPos(), pathfinding.FindEndPos());
        List<Tiles> path = new List<Tiles>();
        path = pathfinding.FindPath(sizeMap);
        path1 = path;
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("ups");
            if (path1 != null && path1.Count > 0)
            {
                foreach (var hexe in path1)
                {
                    
                    Debug.Log($"Tile at {hexe.Position} with color {hexe.TileType}");
                }
            }
            else
            {
                Debug.Log("Lista Tiles jest pusta.");
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("ups");
            if (hexes != null && hexes.Count > 0)
            {
                foreach (var hexe in hexes)
                {
                    foreach (Tiles neigh in hexe.neighbours)
                        
                        Debug.Log($"Tile at {hexe.Position} have neighbour {neigh.Position}");
                }
            }
            else
            {
                Debug.Log("Lista Tiles jest pusta.");
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("ups");
            if (hexes != null && hexes.Count > 0)
            {
                foreach (var hexe in hexes)
                {
                 
                    Debug.Log($"Tile at {hexe.Position} with color {hexe.TileType}");
                }
            }
            else
            {
                Debug.Log("Lista Tiles jest pusta.");
            }
        }

    }
    public List<Tiles> getNeighbours(List<Tiles> tailsList)
    {
        Vector3 newNeighbourPos;
        foreach (var hexe in tailsList)
        {
            Tiles tileToFind = null;
            newNeighbourPos = hexe.Position;
            newNeighbourPos.z = hexe.Position.z + Mathf.Sqrt(3) / 2;
            tileToFind = tailsList.Find(t => t.transform.position == newNeighbourPos);
            if (tileToFind != null)
            {
                hexe.neighbours.Add(tileToFind);
            }
            newNeighbourPos = hexe.Position;
            newNeighbourPos.z = hexe.Position.z - Mathf.Sqrt(3) / 2;
            tileToFind = tailsList.Find(t => t.transform.position == newNeighbourPos);
            if (tileToFind != null)
            {
                hexe.neighbours.Add(tileToFind);
            }
            newNeighbourPos = hexe.Position;
            newNeighbourPos.z = hexe.Position.z + Mathf.Sqrt(3) / 4;
            newNeighbourPos.x = hexe.Position.x + 1.5f / 2;
            tileToFind = tailsList.Find(t => t.transform.position == newNeighbourPos);
            if (tileToFind != null)
            {
                hexe.neighbours.Add(tileToFind);
            }
            newNeighbourPos = hexe.Position;
            newNeighbourPos.z = hexe.Position.z - Mathf.Sqrt(3) / 4;
            newNeighbourPos.x = hexe.Position.x + 1.5f / 2;
            tileToFind = tailsList.Find(t => t.transform.position == newNeighbourPos);
            if (tileToFind != null)
            {
                hexe.neighbours.Add(tileToFind);
            }
            newNeighbourPos = hexe.Position;
            newNeighbourPos.z = hexe.Position.z + Mathf.Sqrt(3) / 4;
            newNeighbourPos.x = hexe.Position.x - 1.5f / 2;
            tileToFind = tailsList.Find(t => t.transform.position == newNeighbourPos);
            if (tileToFind != null)
            {
                hexe.neighbours.Add(tileToFind);
            }
            newNeighbourPos = hexe.Position;
            newNeighbourPos.z = hexe.Position.z - Mathf.Sqrt(3) / 4;
            newNeighbourPos.x = hexe.Position.x - 1.5f / 2;
            tileToFind = tailsList.Find(t => t.transform.position == newNeighbourPos);
            if (tileToFind != null)
            {
                hexe.neighbours.Add(tileToFind);
            }
        }
        return tailsList;
    }

    
}