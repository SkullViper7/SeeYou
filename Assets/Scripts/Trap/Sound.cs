using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField]
    private AudioSource sound;
    [SerializeField]
    private AudioClip clip;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            sound.PlayOneShot(clip);
            if(!sound.isPlaying)
            {
                Destroy(this.gameObject);
            }
            Debug.Log("Bruit");
        }
    }
}
