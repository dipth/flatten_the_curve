using System.Collections;
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
    bool isOpen = true;

    // Start is called before the first frame update
    void Start()
    {
        this.RefreshSprite();
        this.RefreshCapacityLabel();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            Enter();
        }
    }

    void Enter()
    {
        if (!this.isOpen)
        {
            return;
        }

        this.currentCapacity++;
        
        if(currentCapacity == maxCapacity)
        {
            this.isOpen = false;
            RefreshSprite();
        }

        RefreshCapacityLabel();
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
