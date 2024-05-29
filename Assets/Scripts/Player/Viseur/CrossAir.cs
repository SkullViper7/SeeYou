using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossAir : MonoBehaviour
{
    public GameObject Cross;
    public GameObject Point;

    public GameObject ModifCross;
    public GameObject ModifPoint;


    public void ChoosePoint()
    {
        Cross.SetActive(false);
        Point.SetActive(true);
        ModifCross.SetActive(true);
    }

    public void ChooseCross()
    {
        Point.SetActive(false);
        Cross.SetActive(true);
        ModifPoint.SetActive(true);
    }
}
