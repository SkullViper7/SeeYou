using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource sound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Environement")
        {
            sound.Play();
        }
    }
}
