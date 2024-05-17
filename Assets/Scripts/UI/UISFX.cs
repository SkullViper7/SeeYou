using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UISFX : MonoBehaviour
{
    [SerializeField] AudioClip _hover;
    [SerializeField] AudioClip _click;
    [SerializeField] AudioClip _back;

    AudioSource _audioSource;

    Animator _animator;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    public void Hover()
    {
        _audioSource.PlayOneShot(_hover);
        _animator.Play("Hover");
    }

    public void EndHover()
    {
        _animator.Play("EndHover");
    }

    public void Click()
    {
        _audioSource.PlayOneShot(_click);
    }

    public void Back()
    {
        _audioSource.PlayOneShot(_back);
    }
}
