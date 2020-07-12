using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayGun : MonoBehaviour
{
    [SerializeField] ParticleSystem sprayParticles;
    [SerializeField] Collider2D sprayCollider;
    [SerializeField] PlayerMove player;

    // Start is called before the first frame update
    void Start()
    {
        sprayCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            FireGun();
        }
    }

    private void FixedUpdate()
    {
        //transform.rotation = Quaternion.LookRotation(player.moveDir);
        if (player.moveDir.magnitude == 0)
            transform.up = new Vector2(0, -1);
        else
            transform.up = player.moveDir;
    }

    private void FireGun()
    {
        sprayCollider.enabled = true;
        sprayParticles.Play();
        StartCoroutine(EndFireGun());
    }

    private IEnumerator EndFireGun()
    {
        yield return new WaitForSeconds(0.2f);
        sprayCollider.enabled = false;
    }
}
