using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoseColor : MonoBehaviour
{
    public Image CrossAir;
    public Color[] color;
    public Dropdown drop;

    public void ChangeColor(Dropdown myDD)
    {
        CrossAir.color = color[myDD.value];
    }
}
