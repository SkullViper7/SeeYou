using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSetting : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown _resolutionDropdown;

    [SerializeField]
    TMP_Dropdown _screenModeDropdown;

    [SerializeField]
    TMP_Dropdown _FPSDropdown;

    [SerializeField]
    TMP_Dropdown _qualityDropdown;

    int _xResolution;
    int _yResolution;

    bool _isFullscreen;

    int _framerate;

    int _quality;

    public void GetScreenResolution()
    {
        if (_resolutionDropdown.options[_resolutionDropdown.value].text == "4K")
        {
            _xResolution = 3840;
            _yResolution = 2160;
        }

        if (_resolutionDropdown.options[_resolutionDropdown.value].text == "2K")
        {
            _xResolution = 2560;
            _yResolution = 1440;
        }

        if (_resolutionDropdown.options[_resolutionDropdown.value].text == "1080")
        {
            _xResolution = 1920;
            _yResolution = 1080;
        }
    }

    public void GetScreenMode()
    {
        if (_screenModeDropdown.options[_resolutionDropdown.value].text == "Fullscreen")
        {
            _isFullscreen = true;
        }

        if (_screenModeDropdown.options[_resolutionDropdown.value].text == "Windowed")
        {
            _isFullscreen = false;
        }
    }

    public void GetFPSLimiter()
    {
        if (_FPSDropdown.options[_FPSDropdown.value].text == "Unlimited")
        {
            _framerate = 0;
        }

        if (_FPSDropdown.options[_FPSDropdown.value].text == "144")
        {
            _framerate = 144;
        }

        if (_FPSDropdown.options[_FPSDropdown.value].text == "60")
        {
            _framerate = 60;
        }
    }

    public void GetQuality()
    {
        if (_qualityDropdown.options[_qualityDropdown.value].text == "High")
        {
            _quality = 2;
        }

        if (_qualityDropdown.options[_qualityDropdown.value].text == "Medium")
        {
            _quality = 1;
        }

        if (_qualityDropdown.options[_qualityDropdown.value].text == "Low")
        {
            _quality = 0;
        }
    }

    public void ApplyScreenSettings()
    {
        Screen.SetResolution(_xResolution, _yResolution, _isFullscreen);
        Application.targetFrameRate = _framerate;
        QualitySettings.SetQualityLevel(_quality);

        PlayerPrefs.SetInt("xResolution", _xResolution);
        PlayerPrefs.SetInt("yResolution", _yResolution);
        PlayerPrefs.SetString("isFullScreen", _isFullscreen.ToString());
        PlayerPrefs.SetInt("Framerate", _framerate);
        PlayerPrefs.SetInt("Quality", _quality);
    }
}