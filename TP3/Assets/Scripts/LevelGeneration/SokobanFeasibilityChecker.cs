using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SokobanFeasibilityChecker
{

    // CHECK FAISABILITE DU NIVEAU 

    public static bool IsLevelFeasible(Level level)
    {
        // 1. V�rification des Chemins Accessibles
        if (!AreAllBoxesReachable(level))
        {
            return false;
        }

        // 2. V�rification des Configurations Bloqu�es
        if (AreAnyBoxesBlocked(level))
        {
            return false;
        }

        // 3. V�rification de l'�tat Initial
        if (IsInitialStateBlocked(level))
        {
            return false;
        }

        return true;
    }

    // Check si toute les box sont atteignable 
    private static bool AreAllBoxesReachable(Level level)
    {
        foreach (var box in level.GetTilesOfType(TileType.Box))
        {
            if (!IsBoxReachable(level, box.Position))
            {
                return false;
            }
        }
        return true;
    }

    private static bool IsBoxReachable(Level level, Vector2Int boxPosition)
    {
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(boxPosition);
        visited.Add(boxPosition);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var direction in Directions)
            {
                var neighbour = current + direction;

                if (!level.IsInsideBounds(neighbour) || visited.Contains(neighbour))
                {
                    continue;
                }

                if (level.Tiles[neighbour.x, neighbour.y].tileType == TileType.Goal)
                {
                    return true;
                }

                if (level.Tiles[neighbour.x, neighbour.y].tileType == TileType.Floor)
                {
                    queue.Enqueue(neighbour);
                    visited.Add(neighbour);
                }
            }
        }

        return false;
    }

    private static readonly Vector2Int[] Directions = {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1)
    };



    // V�erification si une bo�te est dans une position bloqu�e
    private static bool AreAnyBoxesBlocked(Level level)
    {
        foreach (var box in level.GetTilesOfType(TileType.Box))
        {
            if (IsBoxBlocked(level, box.Position))
            {
                return true;
            }
        }
        return false;
    }

    private static bool IsBoxBlocked(Level level, Vector2Int boxPosition)
    {
        // V�rifie si la bo�te est dans un coin
        if (IsBoxInCorner(level, boxPosition))
        {
            return true;
        }

        // V�rifie si la bo�te est le long d'un mur dans une configuration qui la rend immobile
        if (IsBoxAlongWall(level, boxPosition))
        {
            return true;
        }

        return false;
    }

    private static bool IsBoxInCorner(Level level, Vector2Int position)
    {
        foreach (var direction in CornerDirections)
        {
            var neighbour1 = position + direction[0];
            var neighbour2 = position + direction[1];

            if (level.IsInsideBounds(neighbour1) && level.IsInsideBounds(neighbour2))
            {
                if ((level.Tiles[neighbour1.x, neighbour1.y].tileType == TileType.Wall || level.Tiles[neighbour1.x, neighbour1.y].tileType == TileType.Box) &&
                    (level.Tiles[neighbour2.x, neighbour2.y].tileType == TileType.Wall || level.Tiles[neighbour2.x, neighbour2.y].tileType == TileType.Box))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private static bool IsBoxAlongWall(Level level, Vector2Int position)
    {
        // Impl�mentez ici la logique pour v�rifier si la bo�te est le long d'un mur dans une configuration qui la rend immobile
        return false;
    }

    private static readonly Vector2Int[][] CornerDirections = {
        new[] { Vector2Int.up, Vector2Int.left },
        new[] { Vector2Int.up, Vector2Int.right },
        new[] { Vector2Int.down, Vector2Int.left },
        new[] { Vector2Int.down, Vector2Int.right }
    };

    // v�rification si l'�tat initial bloque le niveau
    private static bool IsInitialStateBlocked(Level level)
    {
        Vector2Int? playerPositiontemp = FindPlayerPosition(level);
        if (playerPositiontemp == null)
        {
            Debug.LogError("Le joueur est introuvable dans le niveau !");
            return true;
        }
        Vector2Int playerPosition = playerPositiontemp.Value;
        // V�rifie si le joueur est bloqu�
        if (IsPlayerBlocked(level, playerPosition))
        {
            return true;
        }

        // V�rifie si une ou plusieurs bo�tes sont imm�diatement bloqu�es
        if (AreAnyBoxesBlocked(level))
        {
            return true;
        }

        return false;
    }

    private static Vector2Int? FindPlayerPosition(Level level)
    {
        for (int x = 0; x < level.Size.x; x++)
        {
            for (int y = 0; y < level.Size.y; y++)
            {
                if (level.Tiles[x, y].tileType == TileType.Player)
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return null;
    }

    private static bool IsPlayerBlocked(Level level, Vector2Int playerPosition)
    {
        foreach (var direction in Directions)
        {
            var neighbour = playerPosition + direction;
            if (level.IsInsideBounds(neighbour) && (level.Tiles[neighbour.x, neighbour.y].tileType == TileType.Floor || level.Tiles[neighbour.x, neighbour.y].tileType == TileType.Goal))
            {
                return false;
            }
        }
        return true;
    }

    public static bool WouldBlockPath(Level level, Vector2Int wallPosition)
    {
        // Sauvegarder l'�tat actuel de la tuile
        Tile originalTile = level.Tiles[wallPosition.x, wallPosition.y];

        // Placer temporairement le mur
        level.SetTile(wallPosition, TileType.Wall);

        // V�rifier si toutes les bo�tes sont accessibles et peuvent atteindre un objectif
        bool allBoxesReachable = AreAllBoxesReachable(level);

        // Remettre la tuile � son �tat original
        level.SetTile(wallPosition, originalTile.tileType);

        // Si toutes les bo�tes sont accessibles, placer le mur ne bloque pas le passage
        return !allBoxesReachable;
    }

    static Collider2D[] cols;
    public static bool isObstacle(Vector2 wallPos)
    {
        cols = Physics2D.OverlapPointAll(wallPos);
        foreach (Collider2D col in cols)
        {
            if (!col.isTrigger)
            {
                return true;
            }
        }
        return false;
    }

    public static bool isCube(Vector2 wallPos)
    {
        cols = Physics2D.OverlapPointAll(wallPos);
        foreach (Collider2D col in cols)
        {
            if (col.CompareTag("Cube")) return true;
        }
        return false;
    }

    public static GameObject GetCubeObject(Vector2 wallPos)
    {
        cols = Physics2D.OverlapPointAll(wallPos);
        foreach (Collider2D col in cols)
        {
            if (col.CompareTag("Cube")) return col.gameObject;
        }
        return null;
    }


    public static bool isWall(Vector2 wallPos)
    {
        cols = Physics2D.OverlapPointAll(wallPos);
        foreach (Collider2D col in cols)
        {
            if (col.CompareTag("Wall")) return true;
        }
        return false;
    }

    
}
