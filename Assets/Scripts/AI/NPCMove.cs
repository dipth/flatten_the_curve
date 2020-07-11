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
    private GameObject player;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        moveSpeed = walkingSpeed;
        moveDir = GetNewDirection();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GettingSprayed();
        }

        HandleState();
        HandleAvoidance();
        HandleMovement();
        HandleAnimation();
        HandleBehaviour();

        Debug.Log(rigidbody2D.velocity);
    }

    private void HandleBehaviour()
    {
        if (state == NPCState.interacting)
        {
            if (behaviour == NPCBehaviour.party)
            {
                rigidbody2D.velocity = new Vector2(0, 0);
                animator.SetBool("Interact", true);
            }
        }
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
                if(CheckForDoor() != null)
                {

                }
                else 
                {
                    moveDir = transform.position - player.transform.position;
                    moveDir = SnapTo(moveDir, 45);
                }
                break;
            case NPCState.interacting:
                moveSpeed = 0f;
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

    Door CheckForDoor() 
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 5, doorMask);
        if (collider2Ds.Length > 0)
        {
            Door destDoor = null;
            float minDist = Mathf.Infinity;
            for (int i = 0; i < collider2Ds.Length; i++)
            {
                destDoor = collider2Ds[i].GetComponent<Door>();
                float dist = Vector2.Distance(transform.position, collider2Ds[i].transform.position);
                if (dist < minDist && destDoor.isOpen)
                {
                    minDist = dist;
                }
            }

            return minDist < Mathf.Infinity ? destDoor : null;
        }
        return null;
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
        if (state != NPCState.fleeing)
            state = NPCState.fleeing;
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = moveDir * moveSpeed;
    }

    Vector2 SnapTo(Vector2 v2, float snapAngle)
    {
        float angle = -Mathf.Atan2(v2.x, v2.y) * Mathf.Rad2Deg + snapAngle;
        angle = Mathf.Round(angle / snapAngle) * snapAngle;
        Quaternion q = Quaternion.AngleAxis(angle, Vector2.up);

        return q * v2;
    }
}
