using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    private PlayerMain main;

    [SerializeField] LayerMask _layerMask;
    [SerializeField] bool _enabled = false;

    RaycastHit _hits;

    public void Shooting()
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

    public void SyncShoot()
    {
        main.playerNetwork.SyncShootServerRpc();
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        _PM.shoot = this;
        main = _PM;
    }
}
