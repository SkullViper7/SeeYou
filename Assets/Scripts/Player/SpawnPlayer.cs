using UnityEngine;
using System.Threading.Tasks;


public class SpawnPlayer : MonoBehaviour
{
    public async void Spawn()
    {
        if (SpawnManager.Instance != null) 
        {
            GameObject spawner = await SpawnManager.Instance.GiveSpawnToAPlayer();
            transform.position = new Vector3(spawner.transform.position.x, 0.7f, spawner.transform.position.z);
            Debug.Log("Player position = " + transform.position + ", spawner position = " + spawner.transform.position);
        }
        else
        {
            Debug.LogError("C'est pas spawn mon reuf");
        }
    }

    public void Spawn(Vector3 _spawner)
    {
        transform.position = new Vector3(_spawner.x, 0.7f, _spawner.z);
        Debug.Log("Player position = " + transform.position + ", spawner position = " + _spawner);
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        _PM.spawnPlayer = this;
    }
}
