using UnityEngine;

public class VFXPlay : MonoBehaviour
{
    BoxCollider _collider;
    ParticleSystem _particleSystem;

    AudioSource _audioSource;

    [SerializeField] AudioClip _sfx;

    /// <summary>
    /// Initializes the VFXPlay component.
    /// Retrieves the BoxCollider and ParticleSystem components from the children.
    /// Retrieves the AudioSource component from the current game object.
    /// </summary>
    private void Awake()
    {
        // Get the BoxCollider component from the child with the BoxCollider component.
        _collider = GetComponentInChildren<BoxCollider>();

        // Get the ParticleSystem component from the child with the ParticleSystem component.
        _particleSystem = GetComponentInChildren<ParticleSystem>();

        // Get the AudioSource component from the current game object.
        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Called when a collision occurs.
    /// Plays the particle system and audio source if the collided object is tagged as "Terrain".
    /// </summary>
    /// <param name="collision">The collision data.</param>
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is tagged as "Terrain"
        if (collision.gameObject.tag == "Terrain")
        {
            // Play the particle system
            _particleSystem.Play();

            // Play the audio source
            _audioSource.PlayOneShot(_sfx);
        }
    }
}
