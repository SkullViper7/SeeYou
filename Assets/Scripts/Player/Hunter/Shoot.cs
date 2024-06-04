using UnityEngine;
using UnityEngine.VFX;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public float DelayBulletBeforeGetDestroy;

    [SerializeField]
    private Transform shoot;

    [SerializeField] 
    private float power;

    private GameObject fire;
    private PlayerMain main;

    public VisualEffect Vfx;

    public AudioClip[] Sounds;
    public AudioSource MyAudioSource;

    private void Start()
    {
        MyAudioSource = GetComponent<AudioSource>();
    }

    public void Shooting()
    {
        AudioClip clip = Sounds[Random.Range(0, Sounds.Length)];
        MyAudioSource.PlayOneShot(clip);
        if (Vfx != null)
        {
            Vfx.Play();
        }

        GameObject boule = Instantiate(bullet, shoot.position, Quaternion.identity);
        boule.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * power);
        boule.SendMessage("InitBullet", gameObject);

        Destroy(boule, DelayBulletBeforeGetDestroy);
    }

    public void SyncShoot()
    {
        main.playerNetwork.SyncShootServerRpc();
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
         _PM.shoot = this;
        main = _PM;
    }

}