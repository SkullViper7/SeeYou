using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collide : MonoBehaviour
{
    private GameObject item;

    Rigidbody rb;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Prey")
        {
            other.gameObject.GetComponent<ProjectileThrow>().GetAnotherItem(gameObject);
        }
    }
}
