using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public Tile[,] Tiles;
    public Vector2Int Size;

    public Level(Vector2Int size)
    {
        Size = size;
        Tiles = new Tile[size.x, size.y];
    }

    public void SetTile(Vector2Int position, TileType type)
    {
        if (IsInsideBounds(position))
        {
            Tiles[position.x, position.y] = new Tile(type, position);
        }
    }

    public bool IsInsideBounds(Vector2Int position)
    {
        return position.x >= 0 && position.x < Size.x && position.y >= 0 && position.y < Size.y;
    }

    public List<Tile> GetTilesOfType(TileType type)
    {
        List<Tile> tilesOfType = new List<Tile>();
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                if (Tiles[x, y] != null && Tiles[x, y].tileType == type)
                {
                    tilesOfType.Add(Tiles[x, y]);
                }
            }
        }
        return tilesOfType;
    }

    public void GeneratesTilesObjects()
    {
        foreach (Tile tile in Tiles)
        {
            tile.CreateTileObj();
        }
    }

}
