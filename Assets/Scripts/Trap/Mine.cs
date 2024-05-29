using System.Collections;
using UnityEngine;

public class MinePoint : Trap
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

    void Start()
    {
        StartCoroutine(Blink());
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
        }
        
        base.OnTriggerEnter(other);
    }

    public override void TriggerEvent()
    {
        Destroy(_playerWhoTriggered);
            _particleSystem.Play();
            zone.SetActive(true);
            ImpulseManager.Instance.Shake(2, 3, new Vector3(0.25f, 0.25f, 0.25f), 0.5f);
            _mesh.SetActive(false);
            _audioSource.PlayOneShot(_sfx);
    }

}
