using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTrigger : MonoBehaviour
{
    public Animator animator;
    public int minTime; // Temps minimum avant le déclenchement de l'animation
    public int maxTime; // Temps maximum avant le déclenchement de l'animation

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
