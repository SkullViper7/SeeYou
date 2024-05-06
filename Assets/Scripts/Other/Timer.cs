using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float time;

    //public GameObject interfaceWin;
    //public GameObject interfaceGame;

    private void FixedUpdate()
    {
        DownTimer();
    }

    public void DownTimer()
    {
        time -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        /*if (time <= 0)
        {
            timerText.color = ChoseColor.red;
            time = 0;
            Time.timeScale = 0;
            interfaceWin.SetActive(true);
            interfaceGame.SetActive(false);
        }*/
    }
}
