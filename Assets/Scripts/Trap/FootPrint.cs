using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FootPrint : MonoBehaviour
{
    [SerializeField] Transform player;

    //[SerializeField] GameObject target;
    [SerializeField] GameObject foot;


    // Update is called once per frame
    void Update()
    {
        //transform.position = player.position;
    }



    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Print());
        }
    }


    IEnumerator Print()
    {
        Instantiate(foot, player.position, transform.rotation);
        yield return new WaitForSeconds(2f);
    }


}
