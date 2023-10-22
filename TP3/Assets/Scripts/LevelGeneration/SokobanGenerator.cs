using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SokobanGenerator : MonoBehaviour
{
    public Vector2Int LevelSize = new Vector2Int(10, 10);
    public int NumberOfGoals = 3;
    public int NumberOfInternalWalls = 3;

    public Level GenerateLevel()
    {
        
        Level level = new Level(LevelSize);

        // Étape 3.1: Construire une Salle Vide
        BuildEmptyRoom(level);

        // Étape 3.2: Placer les Objectifs
        PlaceGoals(level, NumberOfGoals);

        // Étape 3.3: Placer les Boîtes
        PlaceBoxes(level, NumberOfGoals); // Le nombre de boîtes est généralement égal au nombre d'objectifs

        // Étape 3.4: Trouver l'État le Plus Éloigné
        FindFarthestState(level);

        AddInternalWalls(level, NumberOfInternalWalls);

        if (SokobanFeasibilityChecker.IsLevelFeasible(level))
        {
            Debug.Log("Level feasible, generating ...");
            level.GeneratesTilesObjects();
            return level;
        }
        else
        {
            Debug.Log("Level not feasible, create new level");
            return GenerateLevel();
        }
    }


    private void BuildEmptyRoom(Level level)
    {
        for (int x = 0; x < level.Size.x; x++)
        {
            for (int y = 0; y < level.Size.y; y++)
            {
                // Définir les bords comme des murs
                if (x == 0 || y == 0 || x == level.Size.x - 1 || y == level.Size.y - 1)
                {
                    level.SetTile(new Vector2Int(x, y), TileType.Wall);
                }
                else
                {
                    level.SetTile(new Vector2Int(x, y), TileType.Floor);
                }
            }
        }
    }

    private void PlaceGoals(Level level, int numberOfGoals)
    {
        System.Random random = new System.Random();

        for (int i = 0; i < numberOfGoals; i++)
        {
            Vector2Int position;
            do
            {
                position = new Vector2Int(random.Next(1, level.Size.x - 1), random.Next(1, level.Size.y - 1));
            } while (level.Tiles[position.x, position.y].tileType != TileType.Floor);

            level.SetTile(position, TileType.Goal);
        }
    }

    private void FindFarthestState(Level level)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Dictionary<Vector2Int, int> distances = new Dictionary<Vector2Int, int>();

        // Initialiser la queue avec toutes les positions des objectifs
        for (int x = 0; x < level.Size.x; x++)
        {
            for (int y = 0; y < level.Size.y; y++)
            {
                if (level.Tiles[x, y].tileType == TileType.Goal)
                {
                    queue.Enqueue(new Vector2Int(x, y));
                    distances[new Vector2Int(x, y)] = 0;
                }
            }
        }

        Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            foreach (var direction in directions)
            {
                Vector2Int neighbour = current + direction;

                if (level.IsInsideBounds(neighbour) && level.Tiles[neighbour.x, neighbour.y].tileType == TileType.Floor && !distances.ContainsKey(neighbour))
                {
                    distances[neighbour] = distances[current] + 1;
                    queue.Enqueue(neighbour);
                }
            }
        }

        // Trouver la position la plus éloignée
        Vector2Int farthestPosition = distances.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        level.SetTile(farthestPosition, TileType.Player);
    }

    private void PlaceBoxes(Level level, int numberOfBoxes)
    {
        System.Random random = new System.Random();

        for (int i = 0; i < numberOfBoxes; i++)
        {
            Vector2Int position;
            do
            {
                position = new Vector2Int(random.Next(1, level.Size.x - 1), random.Next(1, level.Size.y - 1));
            } while (level.Tiles[position.x, position.y].tileType != TileType.Floor);

            level.SetTile(position, TileType.Box);
        }
    }


    private void AddInternalWalls(Level level, int numberOfWalls)
    {
        System.Random random = new System.Random();

        for (int i = 0; i < numberOfWalls; i++)
        {
            Vector2Int position;
            do
            {
                position = new Vector2Int(random.Next(1, level.Size.x - 1), random.Next(1, level.Size.y - 1));
            } while (level.Tiles[position.x, position.y].tileType != TileType.Floor || SokobanFeasibilityChecker.WouldBlockPath(level, position));

            level.SetTile(position, TileType.Wall);
        }
    }










}
