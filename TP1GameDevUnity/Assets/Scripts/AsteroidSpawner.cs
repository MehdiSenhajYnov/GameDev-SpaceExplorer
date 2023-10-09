using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public Transform asteroidParent;
    public int asteroidCount = 10;
    public float spawnRadius = 10f;
    // Spawn Area
    public float spawnAreaWidth = 10f;
    public float spawnAreaHeight = 10f;

    public List<GameObject> asteroids = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnAsteroids(float LeftLimit, float RightLimit, float TopLimit, float BottomLimit)
    {
        foreach (GameObject asteroid in asteroids)
        {
            Destroy(asteroid);
        }
        asteroids.Clear();
        for (int i = 0; i < asteroidCount * LevelGenerator.Level; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(LeftLimit, RightLimit), Random.Range(BottomLimit, TopLimit), 0);
            GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity, asteroidParent);
            asteroids.Add(asteroid);
        }
    }
}
