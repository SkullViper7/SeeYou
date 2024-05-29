using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiePrye : MonoBehaviour
{    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }
}
