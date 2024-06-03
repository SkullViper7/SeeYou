using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAnimaux : MonoBehaviour
{
    Animator _animator;
    public int minTime; 
    public int maxTime; 
    public float timer;

    void Start()
    {
        _animator = GetComponent<Animator>();
        SetRandomTimer();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            _animator.SetFloat("RandomTrigger", Random.Range(0f, 1f));
            SetRandomTimer();
        }
    }

    void SetRandomTimer()
    {
        timer = Random.Range(minTime, maxTime);
    }
}
