using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField]
    private GameObject zone;

    [SerializeField] Material _light;
    [SerializeField] Material _noLight;
    [SerializeField] MeshRenderer _meshRenderer;

    ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Prey")
        {
            //Destroy(collision.gameObject);
            _particleSystem.Play();
            zone.SetActive(true);
            ImpulseManager.Instance.Shake(2, 3, new Vector3(0.25f, 0.25f, 0.25f), 0.5f);
        }
    }
}
