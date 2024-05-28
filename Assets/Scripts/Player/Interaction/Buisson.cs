using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Buisson : MonoBehaviour
{
    public ParticleSystem Feuille;
    public VisualEffect Vfx;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Prey"))
        {
            Vfx.Play();
            Feuille.Play();
        }       
    }
}
