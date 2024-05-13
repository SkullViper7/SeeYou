using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : MonoBehaviour
{
    public float erodeRate;
    public float erodeRefreshRate;
    public float erodeDelay;
    public SkinnedMeshRenderer erodeObject;

    void Start()
    {
        StartCoroutine(ErodeObject());
    }

    IEnumerator ErodeObject()
    {
        yield return new WaitForSeconds(erodeDelay);


        float t = 0;
        while (t < 1)
        {
            t += erodeRate;
            erodeObject.material.SetFloat("_Erode", t);
            yield return new WaitForSeconds(erodeRefreshRate);
        }
    }
}
