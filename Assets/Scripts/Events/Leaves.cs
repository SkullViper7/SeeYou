using UnityEngine;

public class Leaves : MonoBehaviour
{
    /// <summary>
    /// This method is called when a collider enters the trigger.
    /// It starts the particle system of the child with a ParticleSystem component.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Get the ParticleSystem component of the child with the ParticleSystem component
        // and start it playing.
        GetComponentInChildren<ParticleSystem>().Play();
    }
}
