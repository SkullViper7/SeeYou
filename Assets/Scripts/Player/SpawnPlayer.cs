using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    public void Spawn()
    {
        if(SpawnManager.Instance != null) 
        {
            transform.position = SpawnManager.Instance.GiveSpawnToAPlayer().transform.position;
        }
        else
        {
            Debug.Log("C'est spawn mon reuf");
        }
        
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        _PM.spawnPlayer = this;
    }
}
