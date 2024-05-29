using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Samples;
using UnityEngine;

public class SpeedTrap : MonoBehaviour
{
    [SerializeField]
    PreyMovement preyMovement;
    [SerializeField]
    private GameObject zone;
    [SerializeField]
    private float time;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Environement")
        {
            zone.SetActive(true);
        }
    }


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
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
