using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] GameObject _vfx;

    private void Awake()
    {
        Invoke("Explode", 2f);
    }

    public void Explode()
    {
        _vfx.SetActive(true);
        ImpulseManager.Instance.Shake(2, 3, new Vector3(-0.25f, -0.25f, -0.25f), 0.5f);
        StartCoroutine(RumbleManager.Instance.Rumble(1, 0.5f, 0.6f));
    }
}
