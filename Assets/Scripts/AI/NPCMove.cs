using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : MonoBehaviour
{
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float sprintingSpeed;

    [SerializeField] private List<Vector2> rayDirs = new List<Vector2>();
    [SerializeField] private LayerMask avoidanceMask;

    private float moveSpeed;
    private Vector2 moveDir;
    private Rigidbody2D rigidbody2D;
    private Animator animator;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        moveSpeed = walkingSpeed;
        moveDir = GetNewDirection();
    }

    private void Update()
    {
        HandleAvoidance();
        HandleMovement();
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        animator.SetFloat("xVelocity", rigidbody2D.velocity.x);
        animator.SetFloat("yVelocity", rigidbody2D.velocity.y);
    }

    private void HandleMovement()
    {
    }

    private void HandleAvoidance()
    {
        for (int i = 0; i < rayDirs.Count; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirs[i].normalized, 1, avoidanceMask);

            if (hit.collider != null)
            {
                Debug.DrawRay(transform.position, rayDirs[i].normalized, Color.red, Time.deltaTime);
                if (moveDir == rayDirs[i])
                {
                   moveDir = GetNewDirection();
                }
            }
            else
            {
                Debug.DrawRay(transform.position, rayDirs[i].normalized, Color.green, Time.deltaTime);
            }
        }
    }

    Vector2 GetNewDirection() 
    {
        int index = UnityEngine.Random.Range(0, rayDirs.Count);
        return rayDirs[index];
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = moveDir * moveSpeed;
    }
}
