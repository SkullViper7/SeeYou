using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject P;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            P.SetActive(true);
        }
    }
}
