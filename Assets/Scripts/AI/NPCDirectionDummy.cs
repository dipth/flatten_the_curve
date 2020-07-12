using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDirectionDummy : MonoBehaviour
{
    [SerializeField] PlayerDummy player;
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 playerDirection = player.rigidbody2D.position - rigidbody2D.position;
        playerDirection.Normalize();

        Vector2 fleeDirection = playerDirection * -1;
        float fleeAngle = VectorToAngle(fleeDirection);
        float snapAngle = SnapTo45Degrees(fleeAngle);
        Vector2 snapDirection = AngleToVector(snapAngle);

        Debug.DrawRay(rigidbody2D.position, playerDirection, Color.red);
        Debug.DrawRay(rigidbody2D.position, fleeDirection, Color.yellow);
        Debug.DrawRay(rigidbody2D.position, snapDirection, Color.green);
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
}
