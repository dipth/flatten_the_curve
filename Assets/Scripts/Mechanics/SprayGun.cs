using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayGun : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FireGun()
    {
        collider.enabled = true;
        particles.Play();
    }

    private void ScareNPCs()
    {
        // TODO: Implement me
    }
}
