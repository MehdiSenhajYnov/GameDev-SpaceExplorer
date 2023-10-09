using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] float xMin;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isGameStarted) return;
        transform.Translate(speed * Time.deltaTime * Vector3.left);
        if (transform.position.x < xMin)
        {
            Destroy(gameObject);
        }
    }
}
