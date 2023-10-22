using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerAction
{
    public Vector3Int playerposition;
    public GameObject Cube;
    public Vector3Int CubePosition;

    public PlayerAction(Vector3Int playerposition, GameObject Cube, Vector3Int CubePosition)
    {
        this.playerposition = playerposition;
        this.Cube = Cube;
        this.CubePosition = CubePosition;
    }

    public PlayerAction()
    {
    }
}
