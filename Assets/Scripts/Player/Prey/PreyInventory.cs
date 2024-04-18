using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyInventory : MonoBehaviour
{
    public List<GameObject> Object;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            Object.Add(collision.gameObject);
        }
    }
}
