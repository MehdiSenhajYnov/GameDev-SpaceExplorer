using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private bool isMoving = false;
    public Animator animator; // Référence à l'Animator
    public SpriteRenderer spriteRenderer; // Référence au SpriteRenderer
    public bool canMove;



    AnimationState currentAnimState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = rb.position;
        currentAnimState = AnimationState.idle_down;
    }

    void Update()
    {
        if (GameManager.Instance.GamePaused) { return; }
        if (!isMoving)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(horizontal) > 0.5f && Mathf.Abs(vertical) > 0.5f)
                vertical = 0;

            if (Mathf.Abs(horizontal) > 0.5f)
            {
                targetPosition = new Vector2(Mathf.Round(rb.position.x + horizontal), rb.position.y);
                GameObject CubeToPush = SokobanFeasibilityChecker.GetCubeObject(targetPosition);
                if (CubeToPush != null)
                {
                    if (PushCube(CubeToPush.transform, horizontal > 0 ? Vector3.right : Vector3.left))
                    {
                        isMoving = true;

                        // Gérer les animations latérales
                        ChangeAnimation("run_side");
                    } else
                    {
                        ChangeAnimation("idle_side");

                    }
                } else
                {
                    if (!SokobanFeasibilityChecker.isWall(targetPosition))
                    {
                        isMoving = true;

                        // Gérer les animations latérales
                        ChangeAnimation("run_side");
                    } else
                    {
                        ChangeAnimation("idle_side");
                    }
                }
                spriteRenderer.flipX = horizontal > 0; // Inverser le sprite pour la direction gauche
            }
            else if (Mathf.Abs(vertical) > 0.5f)
            {
                targetPosition = new Vector2(rb.position.x, Mathf.Round(rb.position.y + vertical));

                GameObject CubeToPush = SokobanFeasibilityChecker.GetCubeObject(targetPosition);
                if (CubeToPush != null)
                {
                    if (PushCube(CubeToPush.transform, vertical > 0 ? Vector3.up : Vector3.down))
                    {
                        isMoving = true;

                        // Gérer les animations verticales
                        if (vertical > 0)
                        {
                            ChangeAnimation("run_up");
                        }
                        else
                        {
                            ChangeAnimation("run_down");
                        }
                    } else
                    {
                        // Gérer les animations verticales
                        if (vertical > 0)
                        {
                            ChangeAnimation("idle_up");
                        }
                        else
                        {
                            ChangeAnimation("idle_down");
                        }
                    }

                } else
                {

                    if (!SokobanFeasibilityChecker.isWall(targetPosition))
                    { 
                        isMoving = true;

                        // Gérer les animations verticales
                        if (vertical > 0)
                        {
                            ChangeAnimation("run_up");
                        }
                        else
                        {
                            ChangeAnimation("run_down");
                        }
                    } else
                    {
                        // Gérer les animations verticales
                        if (vertical > 0)
                        {
                            ChangeAnimation("idle_up");
                        }
                        else
                        {
                            ChangeAnimation("idle_down");
                        }
                    }

                }
            }
            else
            {
                // Si aucune touche n'est pressée, jouer l'animation idle correspondante
                SetIdleAnimation();
            }


        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.position = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

            if (rb.position == targetPosition)
            {
                isMoving = false;
                SetIdleAnimation();
            }
        }
    }

    void SetIdleAnimation()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal > 0 || vertical > 0 || horizontal < 0 || vertical < 0) return;

        if (currentAnimState == AnimationState.run_side)
        {
            ChangeAnimation("idle_side");
        }
        else if (currentAnimState == AnimationState.run_up)
        {
            ChangeAnimation("idle_up");
        }
        else if (currentAnimState == AnimationState.run_down)
        {
            ChangeAnimation("idle_down");
        }
    }

    void ChangeAnimation(string animationName)
    {
        if (Enum.TryParse(animationName, false, out currentAnimState))
        {
            animator.Play(animationName);
        }
    }

    enum AnimationState
    {
        idle_up,
        idle_down,
        idle_side,
        run_up,
        run_down,
        run_side,
    }

    public bool PushCube(Transform CubeToPush, Vector3 pushDirection)
    {
        Vector3 TargetPosition = CubeToPush.position + (Vector3)pushDirection;
        if (!SokobanFeasibilityChecker.isObstacle(TargetPosition))
        {
            Vector3Int myPos = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
            Vector3Int cubePosInt = new Vector3Int((int)CubeToPush.position.x, (int)CubeToPush.position.y, 0);
            PlayerAction playerAction = new PlayerAction(myPos, CubeToPush.gameObject, cubePosInt);
            GameManager.Instance.AddAction(playerAction);

            CubeToPush.position = TargetPosition;

            return true;
        }
        return false;
    }
}
