using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelGenerator : Singleton<LevelGenerator>
{
    public static int Level = 1;
    public GameObject LimitObject;
    public AsteroidSpawner asteroidSpawner;
    public GameObject PlayerPrefab;

    public Transform TopLeft, BottomRight;

    public GameObject EarthPrefab;
    public TMP_Text LevelText;

    public TMP_Text StartText;
    public static bool GameStarted = false;

    public bool DebugGameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DebugGameStarted = GameStarted;
        if (Input.GetKeyDown(KeyCode.Space) && !GameStarted)
        {
            GameBegin();
        }
    }

    void GameBegin()
    {
        StartText.gameObject.SetActive(false);
        GameStarted = true;
        GameObject player = Instantiate(PlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Camera.main.GetComponent<CameraFollow>().Player = player.transform;
        NewLevel(1);
    }

    public void Lose()
    {
        StartText.gameObject.SetActive(true);
        StartText.text = $"T'as perdu au niveau {Level} ! \nAppuye sur Espace pour recommencer";
        GameStarted = false;
        Level = 1;
    }

    public void NewLevel(int level)
    {
        Level = level;
        asteroidSpawner.SpawnAsteroids(TopLeft.position.x, BottomRight.position.x, TopLeft.position.y, BottomRight.position.y);
        float earthX = Random.Range(TopLeft.position.x, BottomRight.position.x);
        float earthY = Random.Range(TopLeft.position.y, BottomRight.position.y);
        GameObject earth = Instantiate(EarthPrefab, new Vector3(earthX, earthY, 0), Quaternion.identity);
        LevelText.text = "Niveau " + Level;
    }
}
