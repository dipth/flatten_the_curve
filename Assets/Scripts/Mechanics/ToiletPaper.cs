using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletPaper : MonoBehaviour
{
    [SerializeField] private float destroytimer;

    private void Awake()
    {
        Destroy(this.gameObject, destroytimer);
    }
}

