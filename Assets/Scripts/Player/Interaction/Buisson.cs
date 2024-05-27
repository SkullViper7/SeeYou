using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buisson : MonoBehaviour
{
    public ParticleSystem Feuille;
    public GameObject Vfx;

    private void OnCollisionEnter(Collision collision)
    {
        Vfx.SetActive(true);
        Feuille.Play();
    }

}
