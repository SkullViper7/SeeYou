using UnityEngine;

public class Can : Trap
{
    [SerializeField] AudioClip[] _clips;
    AudioSource _audioSource;

    Rigidbody _rb;

    [SerializeField] float _force;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _rb = GetComponent<Rigidbody>();
    }

    public override void TriggerEvent()
    {
        Vector3 randomDir = new Vector3(Random.Range(-_force, _force), 0, Random.Range(-_force, _force));
        _audioSource.PlayOneShot(_clips[Random.Range(0, _clips.Length)]);

        _rb.AddForce(randomDir, ForceMode.Impulse);
    }
}
