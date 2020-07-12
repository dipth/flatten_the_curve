
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    enum playerMoveState
    {
        walking,sprinting
    }

    private playerMoveState moveState;

    [SerializeField]private float walkSpeed;
    [SerializeField]private float sprintSpeed;

    private Animator animator;
    private Rigidbody2D rigidbody2D;
    private float moveSpeed;
    private float stamina = 100f;
    private float staminaRegenTimer;
    private float staminaRegenDelay = 2f;

    public Vector2 moveDir;


    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = walkSpeed;
        moveState = playerMoveState.walking;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleStamina();
        SetPlayerSpeed();
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        animator.SetFloat("xVelocity", rigidbody2D.velocity.x);
        animator.SetFloat("yVelocity", rigidbody2D.velocity.y);
    }

    private void SetPlayerSpeed()
    {
        if (moveState == playerMoveState.sprinting)
            moveSpeed = sprintSpeed;
        else
            moveSpeed = walkSpeed;
    }

    private void HandleStamina()
    {
        if (moveState == playerMoveState.sprinting)
        {
            stamina -= .5f;
        }
        else if(moveState == playerMoveState.walking) 
        {
            if (stamina < 100 && staminaRegenTimer < Time.time)
            {
                stamina += .5f;
            }
        }
    }

    private void HandleInput() 
    {
        if (Input.GetAxisRaw("Sprint") > 0)
        {
            if (stamina > 0)
            {
                moveState = playerMoveState.sprinting;
                staminaRegenTimer = Time.time + staminaRegenDelay;
            }
            else 
            {
                moveState = playerMoveState.walking;
            }
        }
        else
        {
            moveState = playerMoveState.walking;
        }


        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(x, y).normalized;
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = moveDir * moveSpeed;
    }
}
