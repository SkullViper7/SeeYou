using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Can : MonoBehaviour
{
    [SerializeField] AudioClip[] _clips;
    AudioSource _audioSource;

    Rigidbody _rb;

    [SerializeField] float _force;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        Vector3 randomDir = new Vector3(Random.Range(-_force, _force), 0, Random.Range(-_force, _force));

        if (other.gameObject.tag == "Prey")
        {
            _audioSource.PlayOneShot(_clips[Random.Range(0, _clips.Length)]);

            _rb.AddForce(randomDir, ForceMode.Impulse);
        }
    }
}
