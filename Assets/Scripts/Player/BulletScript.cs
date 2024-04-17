using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class BulletScript : NetworkBehaviour
{
    [SerializeField]
    private float _speed = 20f;


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        GetComponent<Rigidbody>().velocity = this.transform.forward * _speed;
    }
}
