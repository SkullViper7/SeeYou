using System.Collections;
using UnityEngine;

public class FootPrintSpawn : MonoBehaviour
{
    [SerializeField] GameObject _footPrint;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] float _spawnRate;

    /// <summary>
    /// This method is called when the script instance is being loaded.
    /// It starts the coroutine to spawn the footprints at a regular interval.
    /// </summary>
    void Start()
    {
        // Start the coroutine to spawn the footprints
        StartCoroutine(Spawn());
    }

    /**
     * This coroutine is responsible for spawning the footprints at a regular interval.
     * It spawns the footprints on the left and right sides of the spawn point, alternating between
     * the two sides on each iteration.
     * @yield The coroutine will yield control back to the Unity engine at each WaitForSeconds call.
     */
    IEnumerator Spawn()
    {
        // Spawn the footprint on the left side of the spawn point
        Instantiate(_footPrint, new Vector3(_spawnPoint.position.x - 0.25f, _spawnPoint.position.y, _spawnPoint.position.z), 
        Quaternion.Euler(90, _spawnPoint.rotation.eulerAngles.y, 0));

        // Wait for the specified amount of time before spawning the next footprint
        yield return new WaitForSeconds(_spawnRate);

        // Spawn the footprint on the right side of the spawn point
        Instantiate(_footPrint, new Vector3(_spawnPoint.position.x + 0.25f, _spawnPoint.position.y, _spawnPoint.position.z), 
        Quaternion.Euler(90, _spawnPoint.rotation.eulerAngles.y, 0));

        // Wait for the specified amount of time before spawning the next footprint
        yield return new WaitForSeconds(_spawnRate);

        // Recursively call the Spawn coroutine to spawn the footprints at the specified rate
        StartCoroutine(Spawn());
    }
}
