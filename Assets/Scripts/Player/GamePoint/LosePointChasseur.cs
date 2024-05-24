using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePointChasseur : MonoBehaviour
{
    public CountPoint _countPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Environnement"))
        {
            _countPoint.Point -= 1;
        }

    }
}
