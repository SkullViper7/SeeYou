using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaves : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponentInChildren<ParticleSystem>().Play();
    }
}
