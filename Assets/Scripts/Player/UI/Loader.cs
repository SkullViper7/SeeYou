using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public string scenes;

    public void Load()
    {
        SceneManager.LoadScene(scenes);
    }
}
