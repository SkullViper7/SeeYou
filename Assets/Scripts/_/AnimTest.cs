using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    AnimationUpdater _animUpdater;

    void Start()
    {
        _animUpdater = GetComponent<AnimationUpdater>();

        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        yield return new WaitForSeconds(1f);

        _animUpdater.UpdatePreyAnimation(1);

        yield return new WaitForSeconds(2f);

        _animUpdater.UpdatePreyAnimation(0);

        StartCoroutine(Anim());
    }
}
