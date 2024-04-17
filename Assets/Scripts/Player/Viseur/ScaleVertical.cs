using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleVertical : MonoBehaviour
{
    public Slider Size;
    public float SizeScaleX;
    public float SizeScaleY;


    void Update()
    {
        /**/

        
    }

    public void truc()
    {
        SizeScaleX = Size.value;
        Vector3 _scale = new Vector3(SizeScaleX, 1f, 1f);
        this.transform.localScale = _scale * 5;
    }

    public void truc2()
    {
        SizeScaleY = Size.value;
        Vector3 _scale2 = new Vector3(1f, SizeScaleY, SizeScaleY);
        this.transform.localScale = _scale2;
    }

}
