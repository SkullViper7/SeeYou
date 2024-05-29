using UnityEngine;
using UnityEngine.VFX;

public class Buisson : Trap
{
    public ParticleSystem Feuille;
    public VisualEffect Vfx;

    public override void TriggerEvent()
    {
        Vfx.Play();
        Feuille.Play();
    }
}
