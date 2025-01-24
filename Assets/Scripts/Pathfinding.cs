using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Pathfinding
{
    private List<Tiles> allTiles;  
    private Tiles startTile;       
    private Tiles endTile;        
    
  
    public Pathfinding(List<Tiles> tiles)
    {
        allTiles = tiles;  
    }

    
    public void SetStartAndEndTiles(Tiles start, Tiles end)
    {
        startTile = start;
        endTile = end;
    }


    public List<Tiles> FindPath( int sizeMap)
    {
        List<Tiles> path = new List<Tiles>();
        int random;
        path.Add(startTile);    
        Tiles actualTile = startTile;
        actualTile.ChangeTileType("sand");
        random = RandomPicker.PickRandom(0, 2);
        for (int i = 0; i < sizeMap; i++)
        {
            random = RandomPicker.PickRandom(0, 2);
            List<Tiles> potential = new List<Tiles>();
            foreach (var nighbourxd in actualTile.neighbours)
            {
                if (nighbourxd.transform.position.z > actualTile.transform.position.z && actualTile.transform.position.x != nighbourxd.transform.position.x)
                {
                    potential.Add(nighbourxd);
                }
            }
            Debug.Log($"max zasieg {potential.Count-1}");
            random = RandomPicker.PickRandom(0, potential.Count-1);
            actualTile = potential[random];
            actualTile.ChangeTileType("sand");
            path.Add(actualTile);

        }

        return path;
    }
    public Tiles FindStartPos()
    {
        Tiles startTiles = allTiles[0]; 
        foreach( Tiles tile in allTiles)
        {
            if(tile.Position.z < startTiles.Position.z)
            {
                startTiles = tile;
            }    
        }
        Debug.Log($"Start: {startTiles.Position}");
        return startTiles;
    }
    public Tiles FindEndPos()
    {
        Tiles endTiles = allTiles[0];
        foreach (Tiles tile in allTiles)
        {
            if (tile.Position.z > endTiles.Position.z)
            {
                endTiles = tile;
            }
        }
        Debug.Log($"End: {endTiles.Position}");
        return endTiles;
    }


}