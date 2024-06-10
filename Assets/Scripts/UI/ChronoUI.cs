using TMPro;
using UnityEngine;

public class ChronoUI : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float chrono;
    private float chronoMax;

    private void Start()
    {
        chronoMax = 15;
        text = GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        if (chrono > 0)
        {
            chrono = chrono - Time.deltaTime;
            
            text.text = "" + chrono.ToString("F0");
        }
        else
        {
            chrono = 0;
            text.text = "" + chrono;
        }
        
    }

    public void SetChrono() 
    {
        if (chronoMax != 15) 
        {
            chronoMax = 15;
        }

        chrono = chronoMax;
    }
}
