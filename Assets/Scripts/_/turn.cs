using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn : MonoBehaviour
{
    public Camera Camera;
    public Vector3 axe;
    public float angle;

    void Update()
    {
        transform.RotateAround(Camera.transform.position, axe, angle * Time.deltaTime);
    }
}
