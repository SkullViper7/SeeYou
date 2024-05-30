using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUpdater : MonoBehaviour
{
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void UpdateAnimation(int state)
    {
        _animator.SetInteger("State", state);
    }

    public void SetTrigger(string trigger)
    {
        _animator.SetTrigger(trigger);
    }
}
