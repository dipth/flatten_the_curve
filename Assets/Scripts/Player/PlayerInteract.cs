using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private int maxHealthPacks = 5;
    [SerializeField] private List<Image> healthPackUIElements = new List<Image>();
    [SerializeField] private Image exposureBar;
    [SerializeField] private LayerMask exposureMask;
    [SerializeField] private float exposureDistance;

    private int currHealthPacks = 0;
    //Magic numbers weeee....
    private float minExposure = 10;
    private float maxExposure = 395;
    private float currExposure = 10;

    private void Update()
    {
        HandleExposure();
    }

    private void HandleExposure()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, exposureDistance, exposureMask);
        if (collider2Ds.Length > 0)
        {
            if (exposureBar.rectTransform.rect.height < maxExposure)
            {
                currExposure += .1f;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currHealthPacks > 0)
            {
                currExposure -= 75f;

                if (currExposure < minExposure)
                {
                    currExposure = minExposure;
                }

                RemoveHealthPack();
            }
        }

        Rect newrect = exposureBar.rectTransform.rect;
        newrect.height = currExposure;
        exposureBar.rectTransform.sizeDelta = new Vector2(newrect.width, newrect.height);

        if (currExposure >= maxExposure)
        {
            GameManager.instance.Die();
        }
    }

    public bool AddHealthPack() 
    {
        if (currHealthPacks < 5)
        {
            ToggleHealthPacks();
            currHealthPacks++;
            return true;
        }
        return false;
    }

    void RemoveHealthPack() 
    {
        if (currHealthPacks > 0)
        {
            currHealthPacks--;
            ToggleHealthPacks();
        }
    }
    void ToggleHealthPacks() 
    {
        healthPackUIElements[currHealthPacks].enabled = !healthPackUIElements[currHealthPacks].enabled;
    }
}
