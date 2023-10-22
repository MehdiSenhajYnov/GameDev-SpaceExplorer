using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements;

public enum TileType { Wall, Floor, Box, Goal, Player }
public class Tile
{
    public TileType tileType;
    public Vector2Int Position;
    public GameObject gameObject;
    public Tile(TileType _tiletype, Vector2Int position)
    {
        tileType = _tiletype;
        Position = position;
    }

    public void CreateTileObj()
    {
        if ((GameManager.Instance.TileObjects.ContainsKey(tileType)))
        {
            if (tileType != TileType.Wall) {
                GameObject tempfloor = GameObject.Instantiate(GameManager.Instance.TileObjects[TileType.Floor]);
                tempfloor.transform.position = (Vector2)Position;
                tempfloor.transform.parent = GameManager.Instance.FloorParent;

            }
            gameObject = GameObject.Instantiate(GameManager.Instance.TileObjects[tileType]);
            gameObject.transform.position = (Vector2)Position;
            if (tileType == TileType.Wall)
            {
                gameObject.transform.parent = GameManager.Instance.WallParent;
            }
            else if (tileType == TileType.Floor)
            {
                gameObject.transform.parent = GameManager.Instance.FloorParent;
            }
            else
            {
                gameObject.transform.parent = GameManager.Instance.OtherParent;
            }
        }

        GameManager.Instance.Goals.Add(gameObject.GetComponent<GoalCheck>());
    }
}
