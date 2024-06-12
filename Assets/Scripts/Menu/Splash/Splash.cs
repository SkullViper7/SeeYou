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
        StartCoroutine(WaitBeforeLoadMenu());
    }

    IEnumerator WaitBeforeLoadMenu()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("MainMenu");

        asyncOperation.allowSceneActivation = false;

        yield return new WaitUntil(() => DynamicGI.isConverged);

        asyncOperation.allowSceneActivation = true;

        yield return null;
    }
}