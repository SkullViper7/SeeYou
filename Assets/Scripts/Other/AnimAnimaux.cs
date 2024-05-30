using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAnimaux : MonoBehaviour
{
    public Animator animator;
    public int minTime; 
    public int maxTime; 
    public float timer;

    void Start()
    {
        SetRandomTimer();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            animator.SetFloat("RandomTrigger", Random.Range(0f, 1f));
            SetRandomTimer();
        }
    }

    void SetRandomTimer()
    {
        timer = Random.Range(minTime, maxTime);
    }
}
