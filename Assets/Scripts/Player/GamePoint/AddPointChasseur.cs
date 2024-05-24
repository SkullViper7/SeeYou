using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AddPointChasseur : MonoBehaviour
{
    public CountPoint _countPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Prey"))
        {
            _countPoint.Point += 4;
        }

    }
}
