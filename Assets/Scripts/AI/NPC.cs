using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class NPC : MonoBehaviour
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

    public float walkingSpeed;
    public float sprintingSpeed;

    [SerializeField] private List<Vector2> rayDirs = new List<Vector2>();
    [SerializeField] private LayerMask avoidanceMask;
    [SerializeField] private LayerMask doorMask;
    [SerializeField] private LayerMask interactMask;

    private float moveSpeed;
    private Vector2 moveDir;
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private GameObject player;

    private Transform centerPoint;

    private float currStuckTimer;
    private float stuckTimer = 3f;

    private bool isStuck = false;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GettingSprayed();
        }
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = moveDir * moveSpeed;
    }

    public void HandleBehaviour()
    {
        float distToTarget = Mathf.Infinity;
        Vector2 target = new Vector2(0,0);
        Transform targetTransform = null;
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 4, interactMask);

        if (collider2Ds.Length > 0)
        {
            
            for (int i = 0; i < collider2Ds.Length; i++)
            {
                if (collider2Ds[i].transform.position == transform.position)
                    return;

                if (collider2Ds[i].CompareTag(this.tag))
                {
                    targetTransform = collider2Ds[i].transform;
                    target = targetTransform.position - transform.position;
                    SetTargetDirection(target);
                    i = collider2Ds.Length;
                }
            }

            if (target != new Vector2(0,0))
            {
                distToTarget = Vector2.Distance(transform.position, targetTransform.position);

                if (distToTarget < 2.5f)
                {
                    
                    animator.SetTrigger("Interacting");
                }
            }
        }
    }

    public void HandleAnimation()
    {
        animator.SetFloat("xVelocity", rigidbody2D.velocity.x);
        animator.SetFloat("yVelocity", rigidbody2D.velocity.y);
    }

    public void HandleAvoidance()
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
    
    public void HandleGettingStuck() 
    {
        if (rigidbody2D.velocity.magnitude < 0.5f && !isStuck)
        {
            SetStuckTimer();
            isStuck = true;
        }

        if (currStuckTimer < Time.time)
        {
            GetNewDirection();
            isStuck = false;
        }
    }
    public void SetStuckTimer() 
    {
        currStuckTimer = Time.time + stuckTimer;
    }
    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void SetTargetDirection(Vector2 newDir) 
    {
        moveDir = GetSnappedDirection(newDir);
    }
    public void SetNewRandomMoveDirection() 
    {
        moveDir = GetNewDirection();
    }
    public void SetMoveDirectionOppositeToPlayer() 
    {
        moveDir = GetOppositePlayerDirection();
    }

    bool CheckRaycast(Vector2 direction) 
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1, avoidanceMask);

        if (hit.collider != null)
            return true;
        else
            return false;
    }

    public void CheckForDoor() 
    {
        Door destDoor = null;
        float minDist = Mathf.Infinity;

        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 1.95f, doorMask);
        if (collider2Ds.Length > 0)
        { 
            for (int i = 0; i < collider2Ds.Length; i++)
            {
                destDoor = collider2Ds[i].GetComponent<Door>();
                float dist = Vector2.Distance(transform.position, collider2Ds[i].transform.position);
                if (dist < minDist && destDoor.isOpen)
                {
                    minDist = dist;
                }
            }

            if (minDist < Mathf.Infinity)
                SetTargetDirection(destDoor.transform.position - transform.position);
            else
                return;

            if (minDist < 1.3f)
            {
                destDoor.Enter();
                Destroy(this.gameObject);
            }
        }
    }

    public void GettingSprayed() 
    {
        animator.SetTrigger("Fleeing");
    }

    private static float VectorToAngle(Vector2 p_vector2)
    {
        if (p_vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
        }
    }

    private float SnapTo45Degrees(float angle)
    {
        return (Mathf.Round(angle / 45f) * 45f);
    }

    private Vector2 AngleToVector(float angle)
    {
        double radians = (Math.PI / 180) * angle;
        Vector2 direction = new Vector2((float)Math.Cos(radians), -(float)Math.Sin(radians));
        return Quaternion.Euler(0, 0, 90.0f) * direction;
    }
    private Vector2 GetOppositePlayerDirection()
    {
        Vector2 fleeDirection = rigidbody2D.position - new Vector2(player.transform.position.x, player.transform.position.y);
        fleeDirection.Normalize();

        float fleeAngle = VectorToAngle(fleeDirection);
        float snapAngle = SnapTo45Degrees(fleeAngle);
        Vector2 snapDirection = AngleToVector(snapAngle);
        return snapDirection;
    }
    private Vector2 GetSnappedDirection(Vector2 newDir)
    {
        Vector2 fleeDirection = newDir;
        fleeDirection.Normalize();

        float fleeAngle = VectorToAngle(fleeDirection);
        float snapAngle = SnapTo45Degrees(fleeAngle);
        Vector2 snapDirection = AngleToVector(snapAngle);
        return snapDirection;
    }
    private Vector2 GetNewDirection()
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
}
