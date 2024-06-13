using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFadeIn : MonoBehaviour
{
    Animator _musicAnim;

    void Start()
    {
        _musicAnim = GameObject.FindGameObjectWithTag("Music").GetComponent<Animator>();
    }

    public void FadeIn()
    {
        _musicAnim.Play("FadeIn");
    }
}
