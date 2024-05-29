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
        float t = 1;
        while (t > 0)
        {
            t -= erodeRate;
            erodeObject.material.SetFloat("_Erode", t);
            yield return new WaitForSeconds(erodeRefreshRate);
        }

        yield return new WaitForSeconds(erodeDelay);


        while (t < 1)
        {
            t += erodeRate;
            erodeObject.material.SetFloat("_Erode", t);
            yield return new WaitForSeconds(erodeRefreshRate);
        }
    }
}
