using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider Slider;
    public GameObject Cross;
    public GameObject Cross2;


    public void OnValueChanged()
    {
        Animator cross = Cross.GetComponent<Animator>();
        cross.SetFloat("New Float", Slider.value);
    }

    public void OnValueChanged2()
    {
        Animator crossLarge = Cross2.GetComponent<Animator>();
        crossLarge.SetFloat("New Float", Slider.value);
    }
}
