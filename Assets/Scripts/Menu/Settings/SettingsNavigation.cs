using UnityEngine;
using UnityEngine.UI;

public class SettingsNavigation : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField]
    GameObject _audio;
    [SerializeField]
    GameObject _video;

    [Header("Toggles")]
    [SerializeField]
    Toggle _audioToggle;
    [SerializeField]
    Toggle _videoToggle;

    /// <summary>
    /// Shows the audio settings menu and hides others.
    /// </summary>
    /// <remarks>
    /// This function is called when the user selects the audio settings menu from the settings menu.
    /// It sets the active state of the audio, video, and accessibility menus accordingly.
    /// It also sets the interactability of the audio, video, and accessibility toggles.
    /// </remarks>
    public void ShowAudio()
    {
        // Set the active state of the audio, video, and accessibility menus.
        _audio.SetActive(true);
        _video.SetActive(false);

        // Set the interactability of the audio, video, and accessibility toggles.
        _audioToggle.interactable = false;
        _videoToggle.interactable = true;
    }

    /// <summary>
    /// Shows the video settings menu and hides others.
    /// </summary>
    /// <remarks>
    /// This function is called when the user selects the video settings menu from the settings menu.
    /// It sets the active state of the audio, video, and accessibility menus accordingly.
    /// It also sets the interactability of the audio, video, and accessibility toggles.
    /// </remarks>
    public void ShowVideo()
    {
        // Set the active state of the audio, video, and accessibility menus.
        _audio.SetActive(false);
        _video.SetActive(true);

        // Set the interactability of the audio, video, and accessibility toggles.
        _audioToggle.interactable = true;
        _videoToggle.interactable = false;
    }

    /// <summary>
    /// Shows the inputs settings menu and hides others.
    /// </summary>
    /// <remarks>
    /// This function is called when the user selects the inputs settings menu from the settings menu.
    /// It sets the active state of the audio, video, and accessibility menus accordingly.
    /// It also sets the interactability of the audio, video, and accessibility toggles.
    /// </remarks>
    public void ShowInputs()
    {
        // Set the active state of the audio, video, and accessibility menus.
        _audio.SetActive(false);
        _video.SetActive(false);

        // Set the interactability of the audio, video, and accessibility toggles.
        _audioToggle.interactable = true;
        _videoToggle.interactable = true;
    }

    /// <summary>
    /// Shows the accessibility settings menu and hides others.
    /// </summary>
    /// <remarks>
    /// This function is called when the user selects the accessibility settings menu from the settings menu.
    /// It sets the active state of the audio, video, and accessibility menus accordingly.
    /// It also sets the interactability of the audio, video, and accessibility toggles.
    /// </remarks>
    public void ShowAccessibility()
    {
        // Set the active state of the audio, video, and accessibility menus.
        _audio.SetActive(false);
        _video.SetActive(false);

        // Set the interactability of the audio, video, and accessibility toggles.
        _audioToggle.interactable = true;
        _videoToggle.interactable = true;
    }
}