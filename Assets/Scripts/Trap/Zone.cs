using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Prey")
        {
            Destroy(other.gameObject);
            Debug.Log("aaaaa");
        }
    }
}
