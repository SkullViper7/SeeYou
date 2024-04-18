using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
      base.OnNetworkSpawn();
    }
}
