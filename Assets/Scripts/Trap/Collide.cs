using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collide : MonoBehaviour
{
    private GameObject item;

    Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log(collision.collider.tag);
            collision.gameObject.BroadcastMessage("GetAnotherItem", GetComponent<Rigidbody>());
        }
    }
}
