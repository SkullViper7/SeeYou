using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField]
    GameObject _settingsPage;
    [SerializeField]
    GameObject _menuPage;

    public void ShowSettings()
    {
        _menuPage.SetActive(false);
        _settingsPage.SetActive(true);
    }

    public void BackToMenu()
    {
        _menuPage.SetActive(true);
        _settingsPage.SetActive(false);
    }
}