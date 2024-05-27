using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZoneObjects : MonoBehaviour
{
    public GameObject[] grenade;

    public int range1;
    public int range2;

    void Update()
    {
        for(int i = 0; i < grenade.Length; i++)
        {
            int randomIndex = Random.Range(0, grenade.Length);
            Vector3 randomSpawnPosition = new Vector3(Random.Range(range1, range2), 5, Random.Range(range1, range2));

            Instantiate(grenade[randomIndex], randomSpawnPosition, Quaternion.identity);
        }
        
    }

    

}
