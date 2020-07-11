using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDummy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    private Vector2 moveDir;
    public Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(x, y).normalized;
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = moveDir * moveSpeed;
        Debug.DrawRay(rigidbody2D.position, rigidbody2D.velocity, Color.red);
    }
}
