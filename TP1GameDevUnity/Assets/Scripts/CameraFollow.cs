using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    // Update is called once per frame
    void Update()
    {
        if (Player == null)
            return;
        transform.position = new Vector3(Player.position.x, Player.position.y, transform.position.z);
    }
}
