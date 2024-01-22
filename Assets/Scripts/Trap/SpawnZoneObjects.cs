using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZoneObjects : MonoBehaviour
{
    public GameObject[] grenade;

    void Update()
    {
        int randomIndex = Random.Range(0, grenade.Length);
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 11), 5, Random.Range(-10, 11));

        Instantiate(grenade[randomIndex], randomSpawnPosition, Quaternion.identity);
    }
}
