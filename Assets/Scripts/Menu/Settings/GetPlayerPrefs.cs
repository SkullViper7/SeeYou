using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GetPlayerPrefs : MonoBehaviour
{
    [SerializeField]
    AudioMixer _audioMixer;

    private void Start()
    {
        if (PlayerPrefs.HasKey("xResolution") && PlayerPrefs.HasKey("yResolution"))
        {
            Screen.SetResolution(PlayerPrefs.GetInt("xResolution"), PlayerPrefs.GetInt("yResolution"), true);

            if (PlayerPrefs.HasKey("isFullscreen"))
            {
                if (PlayerPrefs.GetString("isFullscreen") == "true")
                {
                    Screen.SetResolution(PlayerPrefs.GetInt("xResolution"), PlayerPrefs.GetInt("yResolution"), true);
                }
                else
                {
                    Screen.SetResolution(PlayerPrefs.GetInt("xResolution"), PlayerPrefs.GetInt("yResolution"), false);
                }
            }
        }

        if (PlayerPrefs.HasKey("Quality"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
        }

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            _audioMixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            _audioMixer.SetFloat("SFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20);
        }
    }
}