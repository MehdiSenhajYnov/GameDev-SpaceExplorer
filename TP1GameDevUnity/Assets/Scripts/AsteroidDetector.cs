using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDetector : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Touché {collision.transform.name} tag : {collision.gameObject.tag}");
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("T'as Perdu !");
            //Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Earth"))
        {
            Destroy(collision.gameObject);
            LevelGenerator.GetInstance().NewLevel(LevelGenerator.Level + 1);
        }
    }
}
