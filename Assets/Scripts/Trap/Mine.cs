using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePoint : MonoBehaviour
{
    [SerializeField]
    private GameObject zone;

    [SerializeField] Material _light;
    [SerializeField] Material _noLight;
    [SerializeField] MeshRenderer _meshRenderer;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Prey")
        {
            Destroy(collision.gameObject);
            Debug.Log("aaaaa");
            zone.SetActive(true);
        }
    } 
}
