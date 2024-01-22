using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject zone;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Prey")
        {
            Destroy(collision.gameObject);
            Debug.Log("aaaaa");
            zone.SetActive(true);
        }
    }

}
