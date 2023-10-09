using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : StateMachineBehaviour
{
    /*
    public float MoveSpeed = 5f;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isLookingRight(animator.transform))
        {
            animator.transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
        } else
        {
              animator.transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed);
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    bool isLookingRight(Transform transform)
    {
        return transform.localScale.x > 0;
    }
    */
}
