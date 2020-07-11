using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : MonoBehaviour
{
    private enum NPCState
    {
        walking,fleeing,interacting
    }
    private enum NPCBehaviour 
    {
        wandrer,party,protester
    }

    private NPCState state;
    private NPCBehaviour behaviour;

    [SerializeField] private float walkingSpeed;
    [SerializeField] private float sprintingSpeed;

    [SerializeField] private List<Vector2> rayDirs = new List<Vector2>();
    [SerializeField] private LayerMask avoidanceMask;
    [SerializeField] private LayerMask doorMask;

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

    private void HandleState()
    {
        switch (state)
        {
            case NPCState.walking:
                moveSpeed = walkingSpeed;
                break;
            case NPCState.fleeing:
                moveSpeed = sprintingSpeed;
                break;
            case NPCState.interacting:
                break;
            default:
                break;
        }

        state = NPCState.walking;
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
            if (CheckRaycast(rayDirs[i].normalized))
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

    bool CheckRaycast(Vector2 direction) 
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1, avoidanceMask);

        if (hit.collider != null)
            return true;
        else
            return false;
    }

    Vector2 CheckForDoor() 
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 5, doorMask);
        if (collider2Ds.Length > 0)
        {
            Vector2 dest = new Vector2(0, 0);
            float minDist = Mathf.Infinity;
            for (int i = 0; i < collider2Ds.Length; i++)
            {
                float dist = Vector2.Distance(transform.position, collider2Ds[i].transform.position);
                if (dist < minDist)
                {
                    dest = collider2Ds[i].transform.position;
                    minDist = dist;
                }
            }

            return dest;
        }

        return new Vector2(0,0);
    }

    Vector2 GetNewDirection() 
    {
        for (int i = 0; i < rayDirs.Count; i++)
        {
            int index = UnityEngine.Random.Range(0, rayDirs.Count);

            if (!CheckRaycast(rayDirs[index]))
            {
                return rayDirs[index];
            }
        }

        return rayDirs[0];
    }

    public void GettingSprayed() 
    {
        state = NPCState.fleeing;
        HandleState();
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = moveDir * moveSpeed;
    }
}
