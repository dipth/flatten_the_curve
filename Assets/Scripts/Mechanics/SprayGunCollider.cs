using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayGunCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit");
        GameObject obj = collision.gameObject;

        if (obj.CompareTag("PartyGoer") || obj.CompareTag("Protester") || obj.CompareTag("Regular"))
        {
            obj.GetComponent<NPC>().GettingSprayed();
        }
    }
}
