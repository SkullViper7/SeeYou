using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    void Awake()
    {
        Music[] musicObjects = GameObject.FindObjectsOfType<Music>();
        if (musicObjects.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);    
    }
}
