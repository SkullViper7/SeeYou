using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePointPrey : MonoBehaviour
{
    public CountPoint _countPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            _countPoint.Point -= 3;
        }
    }
}
