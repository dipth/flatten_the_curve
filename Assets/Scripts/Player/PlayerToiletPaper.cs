using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToiletPaper : MonoBehaviour
{
    public GameObject toiletPaper;

    private Vector2 spawnPoint;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PutDownToiletPaper();
        }
    }

    private void PutDownToiletPaper()
    {
        Instantiate(toiletPaper, transform.position, Quaternion.identity);
    }
}
