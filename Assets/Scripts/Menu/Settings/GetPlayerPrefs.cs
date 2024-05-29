using UnityEngine;
using UnityEngine.Audio;

public class GetPlayerPrefs : MonoBehaviour
{
    [SerializeField]
    AudioMixer _audioMixer;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        // Check if resolution and fullscreen settings are stored in PlayerPrefs
        if (PlayerPrefs.HasKey("xResolution") && PlayerPrefs.HasKey("yResolution"))
        {
            // Set the resolution
            Screen.SetResolution(PlayerPrefs.GetInt("xResolution"), PlayerPrefs.GetInt("yResolution"), true);

            // Check if fullscreen setting is stored in PlayerPrefs
            if (PlayerPrefs.HasKey("isFullscreen"))
            {
                // Set fullscreen accordingly
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

        // Check if quality settings are stored in PlayerPrefs
        if (PlayerPrefs.HasKey("Quality"))
        {
            // Set the quality level
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
        }

        // Check if music volume is stored in PlayerPrefs
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            // Set the music volume
            _audioMixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);
        }

        // Check if SFX volume is stored in PlayerPrefs
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            // Set the SFX volume
            _audioMixer.SetFloat("SFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20);
        }
    }

}