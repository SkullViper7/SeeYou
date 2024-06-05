using UnityEngine;
using UnityEngine.VFX;

public class Buisson : Trap
{
    public ParticleSystem Feuille;
    public VisualEffect Vfx;
    [SerializeField]
    private GameObject mesh;

    AudioSource _audioSource;
    [SerializeField] AudioClip[] _clips;

    protected override void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        base.Start();
    }

    public override void TriggerEvent()
    {
        Vfx.Play();
        _audioSource.PlayOneShot(_clips[Random.Range(0, _clips.Length)]);
        Feuille.Play();
        mesh.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
    }
}
