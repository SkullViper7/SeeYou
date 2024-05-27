using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaw : MonoBehaviour
{
    public int MineCount;
    public GameObject Mine;



    void Start()
    {
       for(int i = 0; i < MineCount; i++)
       {
            Vector3 position = Random.insideUnitSphere;
            SpawMine();
       }
    }

    void SpawMine()
    {
        Instantiate(Mine);
    }
}
