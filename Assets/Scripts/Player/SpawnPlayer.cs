using UnityEngine;
using System.Threading.Tasks;


public class SpawnPlayer : MonoBehaviour
{
    public async void Spawn()
    {
        if (SpawnManager.Instance != null) 
        {
            GameObject spawner = await SpawnManager.Instance.GiveSpawnToAPlayer();
            transform.position = spawner.transform.position;
            Debug.LogError($"SpawnPlayer : {transform.position} / {spawner.transform.position} / {Time.frameCount}");
            transform.position = new Vector3(transform.position.x, 0.7f, transform.position.z);
        }
        else
        {
            Debug.LogError("C'est pas spawn mon reuf");
        }
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        _PM.spawnPlayer = this;
    }
}
