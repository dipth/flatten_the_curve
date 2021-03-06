﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    [SerializeField] private int maxCapacity;
    [SerializeField] private Sprite spriteOpen;
    [SerializeField] private Sprite spriteClosed;
    [SerializeField] private Text capacityLabel;

    private int currentCapacity = 0;
    public bool isOpen = true;

    // Start is called before the first frame update
    void Start()
    {
        this.RefreshSprite();
        this.RefreshCapacityLabel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Enter()
    {
        if (!this.isOpen)
        {
            return;
        }

        this.currentCapacity++;
        
        if(currentCapacity == maxCapacity)
        {
            this.CloseDoor();
        }

        RefreshCapacityLabel();
        GameManager.instance.AddCitizen();
    }

    private void CloseDoor()
    {
        this.isOpen = false;
        this.GetComponent<AudioSource>().Play();
        this.RefreshSprite();
    }

    private void RefreshSprite()
    {
        GetComponent<SpriteRenderer>().sprite = this.isOpen ? this.spriteOpen : this.spriteClosed;
    }

    private void RefreshCapacityLabel()
    {
        capacityLabel.text = $"{this.currentCapacity}/{this.maxCapacity}";
    }
}
