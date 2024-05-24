using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;
    [SerializeField] bool _enabled = false;

    RaycastHit _hits;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection (Vector3.forward));


        if(Physics.Raycast (ray, out _hits, 20f, _layerMask, QueryTriggerInteraction.Ignore))
        {
            Debug.Log("Hit Something");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * _hits.distance, Color.green);
            _enabled = true;
        }
        else
        {
            Debug.Log("Hit Nothing");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20f, Color.red);
            _enabled = false;
        }

        if(Input.GetMouseButtonDown(0) && _enabled == true)
        {
            Debug.Log("Toucher");
            Destroy(_hits.transform.gameObject);
        }
    }



}
