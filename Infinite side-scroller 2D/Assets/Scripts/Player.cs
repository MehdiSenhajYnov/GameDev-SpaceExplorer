using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    public Animator GetAnimator()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        return animator;
    }
    // Start is called before the first frame update

    KeyCode JumpKey = KeyCode.Space;
    void Start()
    {
    }

    public void GameStart()
    {
        GetAnimator().SetBool("Run", true);

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isGameStarted) return;

        if (Input.GetKeyDown(JumpKey))
        {
            GetAnimator().SetTrigger("Jumping");
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Pipe"))
        {
            GameManager.instance.YouLose(true);
        }
    }
}
