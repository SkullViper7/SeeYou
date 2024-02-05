using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Samples;
using UnityEngine;

public class SpeedTrap : MonoBehaviour
{
    [SerializeField]
    PreyMovement preyMovement;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Prey")
        {
            preyMovement.speed = 2.5f;
            StartCoroutine(TimeZone());
        }
    }


    IEnumerator TimeZone()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
