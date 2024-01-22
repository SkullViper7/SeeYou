using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Samples;
using UnityEngine;

public class SpeedTrap : MonoBehaviour
{
    PreyMovement preyMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Prey")
        {
            preyMovement.speed = 2.5f;
        }
    }
}
