using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCheck : MonoBehaviour
{

    Collider2D[] cols;
    SpriteRenderer spriteRenderer;
    public bool onGoal;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        onGoal = false;
        cols = Physics2D.OverlapPointAll(transform.position);
        if (cols.Length > 0 )
        {
            foreach ( Collider2D col in cols )
            {
                if ( col.CompareTag("Goal") )
                {
                    onGoal = true;
                    break;
                }
            }
        }

        if ( onGoal )
        {
            spriteRenderer.color = Color.green;
        } else
        { 
            spriteRenderer.color = Color.white;
        }
    }
}
