using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    public bool Dead = false;

    public float speed = 5f;
    public float rotationSpeed = 200f;
    public string UpKey = "w";
    public string DownKey = "s";
    public string LeftKey = "a";
    public string RightKey = "d";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelGenerator.GameStarted) return;
        if (Dead) return;
        if (Input.GetKey(UpKey))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(DownKey))
        {
            transform.Translate(-Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(LeftKey))
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(RightKey))
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
    }
}
