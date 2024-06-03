using UnityEngine;
using UnityEngine.VFX;

public class Buisson : Trap
{
    public ParticleSystem Feuille;
    public VisualEffect Vfx;
    [SerializeField]
    private GameObject mesh;

    public override void TriggerEvent()
    {
        Vfx.Play();
        Feuille.Play();
        mesh.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
    }
}
