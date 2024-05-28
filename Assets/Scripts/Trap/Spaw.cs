using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaw : MonoBehaviour
{
    public List<GameObject> objectsToSpawn;  // Liste des objets à faire apparaître
    public float spawnInterval = 2f;         // Intervalle de temps entre chaque apparition
    public Vector3 spawnPosition;            // Position où faire apparaître les objets

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnRandomObject();
        }
    }

    void SpawnRandomObject()
    {
        if (objectsToSpawn.Count > 0)
        {
            int randomIndex = Random.Range(0, objectsToSpawn.Count);
            Instantiate(objectsToSpawn[randomIndex], spawnPosition, Quaternion.identity);
        }
    }
}
