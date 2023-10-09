using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeat : MonoBehaviour
{
    public Transform TopLeft, BottomRight;
    public bool isPlayerInside;
    Collider2D[] colliders;
    public LayerMask PlayerLayer;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerInside = false;
        colliders = Physics2D.OverlapAreaAll(TopLeft.position, BottomRight.position, PlayerLayer);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != null && collider.gameObject == Player)
            {
                isPlayerInside = true;
            }
        }
        if (!isPlayerInside)
        {
            Destroy(gameObject);
        }
    }
}
