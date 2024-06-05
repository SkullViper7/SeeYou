using System.Collections;
using UnityEngine;

public class Mine : Trap
{
    [SerializeField]
    private GameObject zone;

    [SerializeField] Material _light;
    [SerializeField] Material _noLight;
    [SerializeField] MeshRenderer _meshRenderer;

    ParticleSystem _particleSystem;

    [SerializeField] GameObject _mesh;

    [SerializeField] AudioClip _sfx;
    AudioSource _audioSource;

    private GameObject _playerWhoTriggered;

    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();

        _audioSource = GetComponent<AudioSource>();
    }

    protected override void Start()
    {
        StartCoroutine(Blink());
        base.Start();
    }

    IEnumerator Blink()
    {
        yield return new WaitForSeconds(1f);
        _meshRenderer.material = _light;
        yield return new WaitForSeconds(0.1f);
        _meshRenderer.material = _noLight;

        StartCoroutine(Blink());
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Prey")
        {
            _playerWhoTriggered = other.gameObject;
            Ragdoll _playerRagdoll = other.GetComponent<Ragdoll>();
            if (_playerRagdoll != null) 
            {
                other.GetComponent<Ragdoll>().EnableRagdoll();
            }
        }
        
        base.OnTriggerEnter(other);
    }

    public override void TriggerEvent()
    {
        _particleSystem.Play();
        _mesh.SetActive(false);
        zone.SetActive(true);
        ImpulseManager.Instance.Shake(0, 3, new Vector3(0.25f, 0.25f, 0.25f), 0.5f);
        _audioSource.PlayOneShot(_sfx);
        GameManager.Instance.Items.Remove(gameObject);
    }

}
