using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrintSpawn : MonoBehaviour
{
    [SerializeField] GameObject _footPrint;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] float _spawnRate;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        Instantiate(_footPrint, new Vector3(_spawnPoint.position.x - 0.25f, _spawnPoint.position.y, _spawnPoint.position.z), 
        Quaternion.Euler(90, _spawnPoint.rotation.eulerAngles.y, 0));

        yield return new WaitForSeconds(_spawnRate);

        Instantiate(_footPrint, new Vector3(_spawnPoint.position.x + 0.25f, _spawnPoint.position.y, _spawnPoint.position.z), 
        Quaternion.Euler(90, _spawnPoint.rotation.eulerAngles.y, 0));

        yield return new WaitForSeconds(_spawnRate);

        StartCoroutine(Spawn());
    }
}
