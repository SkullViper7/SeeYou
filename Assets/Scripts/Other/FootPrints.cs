using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrints : MonoBehaviour
{
    [SerializeField] float _lifetime;

    void Start()
    {
        StartCoroutine(Disapear());
    }

    IEnumerator Disapear()
    {
        yield return new WaitForSeconds(_lifetime);

        Destroy(gameObject);
    }
}
