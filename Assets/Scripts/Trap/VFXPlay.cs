using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VFXPlay : MonoBehaviour
{
    BoxCollider _collider;
    ParticleSystem _particleSystem;

    private void Awake()
    {
        _collider = GetComponentInChildren<BoxCollider>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            _particleSystem.Play();
        }
    }
}
