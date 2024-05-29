using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VFXPlay : MonoBehaviour
{
    BoxCollider _collider;
    ParticleSystem _particleSystem;

    AudioSource _audioSource;

    [SerializeField] AudioClip _sfx;

    private void Awake()
    {
        _collider = GetComponentInChildren<BoxCollider>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();

        _audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            _particleSystem.Play();
            _audioSource.PlayOneShot(_sfx);
        }
    }
}
