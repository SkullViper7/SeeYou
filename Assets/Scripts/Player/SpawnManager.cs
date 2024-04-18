using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance = null;
    public static SpawnManager Instance => _instance;

    private List<GameObject> spawnList = new List<GameObject>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        this.initSpawner();
    }

    void initSpawner()
    {
        this.spawnList = GameObject.FindGameObjectsWithTag("Spawner").ToList();

    }

    public GameObject GiveSpawnToAPlayer()
    {
        GameObject spawner = this.spawnList[Random.Range(0, this.spawnList.Count)];
        this.spawnList.Remove(spawner);
        return spawner;
    }

}
