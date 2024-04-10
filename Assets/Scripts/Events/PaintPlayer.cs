using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject _secondCapsule;

    private void Start()
    {
        StartCoroutine(Paint());
    }

    IEnumerator Paint()
    {
        yield return new WaitForSeconds(1.0f);

        _secondCapsule.SetActive(true);

        StartCoroutine(DisableCapsule());
    }

    IEnumerator DisableCapsule()
    {
        yield return new WaitForSeconds(0.5f);

        _secondCapsule.SetActive(false);
    }
}
