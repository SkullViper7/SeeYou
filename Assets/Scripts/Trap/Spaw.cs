using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaw : MonoBehaviour
{
    public List<GameObject> objectsToSpawn;  // Liste des objets � faire appara�tre
    public float spawnInterval = 2f;         // Intervalle de temps entre chaque apparition
    public Vector3 spawnPosition;            // Position o� faire appara�tre les objets

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
