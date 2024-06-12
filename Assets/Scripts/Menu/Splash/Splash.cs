using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    [SerializeField] GameObject _loadingScreen;

    private void Start()
    {
        Invoke("LoadMenu", 4);
    }

    void LoadMenu()
    {
        _loadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync("MainMenu");
    }
}