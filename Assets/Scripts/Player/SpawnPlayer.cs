using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public void Spawn()
    {
        if (SpawnManager.Instance != null) 
        {
            transform.position = SpawnManager.Instance.GiveSpawnToAPlayer().transform.position;
            transform.position = new Vector3(transform.position.x, 0.7f, transform.position.z);
        }
        else
        {
            Debug.Log("C'est pas spawn mon reuf");
        }
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        _PM.spawnPlayer = this;
    }
}
