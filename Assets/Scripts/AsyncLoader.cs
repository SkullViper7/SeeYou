using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoader : MonoBehaviour
{
    [SerializeField]
    GameObject _mainScreen;
    [SerializeField]
    GameObject _loadingScreen;

    public void LoadLevel(string levelToLoad)
    {
        _mainScreen.SetActive(false);
        _loadingScreen.SetActive(true);

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
    }
}