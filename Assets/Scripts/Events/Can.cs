using UnityEngine;

public class Can : MonoBehaviour
{
    [SerializeField] AudioClip[] _clips;
    AudioSource _audioSource;

    Rigidbody _rb;

    [SerializeField] float _force;

    /// <summary>
    /// This method is called when the script instance is being loaded.
    /// </summary>
    /// <remarks>
    /// This method is called when the script instance is being loaded. Awake is called when the script instance
    /// is being loaded. This method is called before any Start methods and allows you to initialize variables and
    /// other data before the game starts.
    /// </remarks>
    private void Awake()
    {
        // Get the AudioSource component attached to the game object
        _audioSource = GetComponent<AudioSource>();

        // Get the Rigidbody component attached to the game object
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// This method is called when this collider/rigidbody has begun touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        // Generate a random direction for the force to be applied
        Vector3 randomDir = new Vector3(Random.Range(-_force, _force), 0, Random.Range(-_force, _force));

        // Check if the collided object has the tag "Prey"
        if (other.gameObject.tag == "Prey")
        {
            // Play a random audio clip from the array
            _audioSource.PlayOneShot(_clips[Random.Range(0, _clips.Length)]);

            // Apply a force to the rigidbody of the collided object in the random direction
            _rb.AddForce(randomDir, ForceMode.Impulse);
        }
    }
}
