using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsNavigation : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField]
    GameObject _audio;
    [SerializeField]
    GameObject _video;
    [SerializeField]
    GameObject _inputs;
    [SerializeField]
    GameObject _accessibility;

    [Header("Toggles")]
    [SerializeField]
    Toggle _audioToggle;
    [SerializeField]
    Toggle _videoToggle;
    [SerializeField]
    Toggle _inputsToggle;
    [SerializeField]
    Toggle _accessibilityToggle;

    public void ShowAudio()
    {
        _audio.SetActive(true);
        _video.SetActive(false);
        _inputs.SetActive(false);
        _accessibility.SetActive(false);

        _audioToggle.interactable = false;
        _videoToggle.interactable = true;
        _inputsToggle.interactable = true;
        _accessibilityToggle.interactable = true;
    }

    public void ShowVideo()
    {
        _audio.SetActive(false);
        _video.SetActive(true);
        _inputs.SetActive(false);
        _accessibility.SetActive(false);

        _audioToggle.interactable = true;
        _videoToggle.interactable = false;
        _inputsToggle.interactable = true;
        _accessibilityToggle.interactable = true;
    }

    public void ShowInputs()
    {
        _audio.SetActive(false);
        _video.SetActive(false);
        _inputs.SetActive(true);
        _accessibility.SetActive(false);

        _audioToggle.interactable = true;
        _videoToggle.interactable = true;
        _inputsToggle.interactable = false;
        _accessibilityToggle.interactable = true;
    }

    public void ShowAccessibility()
    {
        _audio.SetActive(false);
        _video.SetActive(false);
        _inputs.SetActive(false);
        _accessibility.SetActive(true);

        _audioToggle.interactable = true;
        _videoToggle.interactable = true;
        _inputsToggle.interactable = true;
        _accessibilityToggle.interactable = false;
    }
}