using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance = null;
    public static SpawnManager Instance => _instance;

    [SerializeField]
    private List<GameObject> spawnList = new List<GameObject>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
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
        initSpawner();
    }

    void initSpawner()
    {
        //spawnList = GameObject.FindGameObjectsWithTag("Spawner").ToList();

    }

    public async Task<GameObject> GiveSpawnToAPlayer()
    {
        GameObject spawner = spawnList[Random.Range(0, spawnList.Count)];
        spawnList.Remove(spawner);
        Debug.LogError(spawner.transform.position +"/" + spawnList.Count + "/" + spawner.name + "/" + Time.frameCount);

        if(spawner.transform.position == Vector3.zero)
        {
            Debug.Log("zero");
        }

        await Task.Delay(10);

        return spawner;

        // return await Task.Run(() =>
        // {
        //     GameObject spawner = spawnList[Random.Range(0, spawnList.Count)];
        //     spawnList.Remove(spawner);
        //     return spawner;
        // });
    }

}
