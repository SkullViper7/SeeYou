using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    private void Start()
    {
        Invoke("LoadMenu", 4);
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}