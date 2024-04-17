using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassLOD : MonoBehaviour
{
    Transform _playerTransform;

    MeshRenderer _meshRenderer;

    Material _material;

    void Start()
    {
        _playerTransform = GameObject.Find("Player").transform;
        _meshRenderer = GetComponent<MeshRenderer>();
        _material = _meshRenderer.material;
    }

    void Update()
    {
        if (Vector3.Distance(_playerTransform.position, transform.position) > 5)
        {
            _material.SetFloat("_LODFactor", 0);
        }
        else
        {
            _material.SetFloat("_LODFactor", 1);
        }
    }
}
