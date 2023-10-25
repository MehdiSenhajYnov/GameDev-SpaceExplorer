using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : Singleton<TerrainGenerator>
{
    public bool AutoUpdate = false;
    public Sprite WaterTile;
    public Sprite TerrainTile;
    public Sprite ForestTile;
    public Sprite MountainTile;


    public int worldSize = 100;
    public float noiseFreq = 0.5f;
    public float seed;
    public Texture2D noiseTexture;

    public float minForestHeight = 0.2f;
    public float minTerrainHeight = 0.6f;
    public float minMountainHeight = 0.8f;

    private void Start()
    {
        GenerateAll();
    }

    public void GenerateAll()
    {
        seed = Random.Range(-10000, 10000);
        DeleteAll();
        noiseTexture = GetNewNoiseTexture(worldSize,worldSize);
        GenerateTerrain();
    }
    public int StreetsNumber = 4;
    public float WaterChance = 0.2f;
    public float MountainChance = 0.2f;
    public int CenterDimension = 10;

    public bool[,] usedTiles;

    private void GenerateTerrain()
    {
        // Création de la zone centrale
        int centerX = worldSize / 2;
        int centerY = worldSize / 2;
        usedTiles = new bool[worldSize, worldSize];

        for (int x = centerX - CenterDimension; x < centerX + CenterDimension; x++)
        {
            for (int y = centerY - CenterDimension; y < centerY + CenterDimension; y++)
            {
                if (Mathf.PerlinNoise((x + seed) * noiseFreq, (y + seed) * noiseFreq) > 0.3f)
                {
                    InstantiateTile(x, y, TerrainTile, false);
                }
            }
        }

        // Création des ruelles
        for (int i = 0; i < StreetsNumber; i++)
        {
            Vector2 direction = RandomDirectionWithinBounds();
            CreateStreet(centerX, centerY, centerX + (int)direction.x, centerY + (int)direction.y);
        }
        // Remplissage du terrain
        for (int x = 0; x < worldSize; x++)
        {
            for (int y = 0; y < worldSize; y++)
            {
                if (usedTiles[x, y])
                {
                    continue;
                }
                float value = Mathf.PerlinNoise((x + seed) * noiseFreq, (y + seed) * noiseFreq);
                if (value < minForestHeight)
                {
                    InstantiateTile(x, y, WaterTile, true);
                }
                else if (value < minTerrainHeight)
                {
                    InstantiateTile(x, y, ForestTile, true);
                }
                else if (value < minMountainHeight)
                {
                    InstantiateTile(x, y, TerrainTile, false);
                }
                else
                {
                    InstantiateTile(x, y, MountainTile, true);
                }
            }
        }
        usedTiles = null;
        System.GC.Collect();
    }

    private Vector2 RandomDirectionWithinBounds()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float distance = Random.Range(10, worldSize / 2 - 10);
        return new Vector2(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance);
    }

    private void CreateStreet(int startX, int startY, int endX, int endY)
    {
        Vector2 position = new Vector2(startX, startY);
        Vector2 endPosition = new Vector2(endX, endY);
        while ((position - endPosition).sqrMagnitude > 2)
        {
            position = Vector2.MoveTowards(position, endPosition, 1);
            InstantiateTile((int)position.x, (int)position.y, TerrainTile, false);
        }
    }

    public void InstantiateTile(int x, int y, Sprite tileSprite, bool haveCollider)
    {
        GameObject tile = new GameObject($"Tile_{tileSprite.name}_{x}_{y}");
        if (usedTiles != null && x < usedTiles.GetLength(0) && y < usedTiles.GetLength(1))
            usedTiles[x, y] = true;
        tile.transform.parent = this.transform;
        tile.transform.position = new Vector3(x, y, 0);
        SpriteRenderer renderer = tile.AddComponent<SpriteRenderer>();
        renderer.sprite = tileSprite;
        if (haveCollider)
        {             
            var coltemp = tile.AddComponent<BoxCollider2D>();
            coltemp.isTrigger = true;
        }
        if (tileSprite == ForestTile)
        {
            tile.AddComponent<ForestClickable>();
        }
    }

    private Texture2D GetNewNoiseTexture(int xWorld, int yWorld)
    {
        Texture2D TempNoiseTexture = new Texture2D(xWorld, yWorld);

        for (int x = 0; x < TempNoiseTexture.width; x++)
        {
            for (int y = 0; y < TempNoiseTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * noiseFreq, (y + seed) * noiseFreq);
                TempNoiseTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }

        TempNoiseTexture.Apply();
        return TempNoiseTexture;
    }

    public void DeleteAll()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

}
