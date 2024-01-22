using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance = null;
    public static SpawnManager Instance => _instance;

    List<GameObject> spawnList = new List<GameObject>();

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

    // Start is called before the first frame update
    void Start()
    {
        initSpawner();
        Debug.Log("init");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initSpawner()
    {
        spawnList = GameObject.FindGameObjectsWithTag("Spawner").ToList();

    }

    public GameObject GiveSpawnToAPlayer()
    {
        GameObject spawner = spawnList[Random.Range(0, spawnList.Count)];
        Debug.Log(spawner.transform.position);
        spawnList.Remove(spawner);
        return spawner;
    }

}
