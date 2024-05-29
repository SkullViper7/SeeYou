using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    private static RumbleManager _instance;

    public static RumbleManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<RumbleManager>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("RumbleManager");
                    _instance = go.AddComponent<RumbleManager>();
                }
            }

            return _instance;
        }
    }

    /// <summary>
    /// This method is called when the script instance is being loaded.
    /// It ensures that there is only one instance of the RumbleManager in the scene.
    /// </summary>
    private void Awake()
    {
        // If the instance is not null and it's not the same as this,
        // it means that there is already an instance of the RumbleManager in the scene,
        // so we destroy the current game object to avoid having multiple instances.
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// This method triggers a rumble on the gamepad.
    /// </summary>
    /// <param name="intensity">The intensity of the rumble.</param>
    /// <param name="duration">The duration of the rumble in seconds.</param>
    /// <param name="decrease">The decrease in intensity of the rumble per iteration.</param>
    /// <returns>An enumerator that controls the rumble.</returns>
    public IEnumerator Rumble(float intensity, float duration, float decrease)
    {
        // Ensure that there is a gamepad available
        if(Gamepad.current != null)
        {
            float currentIntensity = intensity;

            // Rumble for the specified duration
            while(currentIntensity > 0)
            {
                // Set the motor speeds of the gamepad to the current intensity
                Gamepad.current.SetMotorSpeeds(currentIntensity, currentIntensity);

                // Wait for the specified duration divided by the intensity
                yield return new WaitForSeconds(duration / intensity);

                // Decrease the intensity of the rumble
                currentIntensity -= decrease;
            }

            // Stop the rumble by setting the motor speeds to 0
            Gamepad.current.SetMotorSpeeds(0, 0);
        }
    }
}
