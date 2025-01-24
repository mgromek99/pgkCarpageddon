using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class GameMaster : MonoBehaviour
{
    public int healthLost = 1;

    public GameObject gejmOwer;
    public static GameMaster Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            Debug.Assert(_instance == null, "GameMaster already exists");
            _instance = value;
        }
    }

    public Map map;
    public SpawnEnemy spawnEnemy;
    public HPinterface hpInterFace;
    public GameObject fireOpponent;
    public GameObject waterOpponent;
    public GameObject earthOpponent;
   

    private static GameMaster _instance;
    private int playerHP = 10;
    private void Awake()
    {
        Instance = this;


    }
    // Start is called before the first frame update
    void Start()
    {
        map.GenerateMap();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            spawnEnemy.PrepareSpawn(fireOpponent);

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            spawnEnemy.PrepareSpawn(waterOpponent);

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            spawnEnemy.PrepareSpawn(earthOpponent);

        }
    }
    public void SpawnEnemy(int numerek)
    {
        if(numerek == 0)
        {
            spawnEnemy.PrepareSpawn(earthOpponent);
        }
        else if (numerek == 1)
        {
            spawnEnemy.PrepareSpawn(fireOpponent);
        }
        else
        {
            spawnEnemy.PrepareSpawn(waterOpponent);
        }
    }
    public void LostHealthPoint()
    {
        playerHP -= healthLost;
        hpInterFace.UpdateHealBar(playerHP);
        if (playerHP <= 0)
            gameOver();
    }

    public void gameOver()
    {
        gejmOwer.SetActive(true);
    }
}
