using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    private Rigidbody2D rb2d;
    private Vector2 currentDirection;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        // Choisissez une direction aléatoire au démarrage
        float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad; 
        currentDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;

        MoveInCurrentDirection();
    }

    private void MoveInCurrentDirection()
    {
        rb2d.velocity = currentDirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // Choisissez une direction aléatoire au démarrage
        float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
        currentDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;

        MoveInCurrentDirection();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        // Choisissez une direction aléatoire au démarrage
        float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
        currentDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;

        MoveInCurrentDirection();
    }

    private void Update()
    {
        if (!LevelGenerator.GameStarted)
        {
            rb2d.velocity = Vector2.zero;
        }
    }

}
