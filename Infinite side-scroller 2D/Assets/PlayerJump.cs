using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : StateMachineBehaviour
{

    KeyCode JumpKey = KeyCode.Space;
    Collider2D[] colliders;
    Rigidbody2D rigidbody;
    public float JumpForce = 10f;
    Transform groundcheck;
    Transform topcheck;

    public Rigidbody2D GetRB(Animator animator)
    {
        if (rigidbody == null)
        {
            rigidbody = animator.GetComponent<Rigidbody2D>();
        }
        return rigidbody;
    }

    public Transform GetGroundCheck(Animator animator)
    {
        if (groundcheck == null)
        {
            groundcheck = animator.transform.GetChild(0);
        }
        return groundcheck;
    }

    public Transform GetTopCheck(Animator animator)
    {
        if (topcheck == null)
        {
            topcheck = animator.transform.GetChild(1);
        }
        return topcheck;
    }

    bool JumpReleased;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        JumpReleased = false;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKey(JumpKey) && !JumpReleased)
        {
            GetRB(animator).AddForce(Vector2.up * JumpForce);
        }
        if (Input.GetKeyUp(JumpKey))
        {
            JumpReleased = true;
        }

        if (JumpReleased)
        {
            colliders = Physics2D.OverlapPointAll(GetGroundCheck(animator).position);
            if (colliders.Length > 0)
            {
                animator.SetBool("Jumping", false);
            }
        } else
        {
            colliders = Physics2D.OverlapPointAll(GetTopCheck(animator).position);
            if (colliders.Length > 0)
            {
                GetRB(animator).velocity = Vector2.zero;
                JumpReleased = true;
            }
        }

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
