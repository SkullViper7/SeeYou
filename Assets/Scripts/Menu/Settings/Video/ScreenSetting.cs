using TMPro;
using UnityEngine;

public class ScreenSetting : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown _resolutionDropdown;

    [SerializeField]
    TMP_Dropdown _screenModeDropdown;

    [SerializeField]
    TMP_Dropdown _qualityDropdown;

    int _xResolution;
    int _yResolution;

    bool _isFullscreen;

    int _quality;

    public void GetScreenResolution()
    {
        if (_resolutionDropdown.value == 2)
        {
            _xResolution = 3840;
            _yResolution = 2160;
        }

        if (_resolutionDropdown.value == 1)
        {
            _xResolution = 2560;
            _yResolution = 1440;
        }

        if (_resolutionDropdown.value == 0)
        {
            _xResolution = 1920;
            _yResolution = 1080;
        }
    }

    public void GetScreenMode()
    {
        if (_screenModeDropdown.value == 0)
        {
            _isFullscreen = true;
        }

        if (_screenModeDropdown.value == 1)
        {
            _isFullscreen = false;
        }
    }

    public void GetQuality()
    {
        if (_qualityDropdown.value == 0)
        {
            _quality = 2;
        }

        if (_qualityDropdown.value == 1)
        {
            _quality = 1;
        }

        if (_qualityDropdown.value == 2)
        {
            _quality = 0;
        }
    }

    public void ApplyScreenSettings()
    {
        Screen.SetResolution(_xResolution, _yResolution, _isFullscreen);
        QualitySettings.SetQualityLevel(_quality);

        PlayerPrefs.SetInt("xResolution", _xResolution);
        PlayerPrefs.SetInt("yResolution", _yResolution);
        PlayerPrefs.SetString("isFullScreen", _isFullscreen.ToString());
        PlayerPrefs.SetInt("Quality", _quality);
    }
}