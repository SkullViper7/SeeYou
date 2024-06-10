using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUpdater : MonoBehaviour
{
    Animator _preyAnimator;
    [SerializeField] Animator _hunterAnimator;

    private void Awake()
    {
        _preyAnimator = GetComponent<Animator>();
    }

    public void UpdatePreyAnimation(int state)
    {
        _preyAnimator.SetInteger("State", state);
    }

    public void UpdateHunterAnimation(int state)
    {
        _hunterAnimator.SetInteger("State", state);
    }
}
