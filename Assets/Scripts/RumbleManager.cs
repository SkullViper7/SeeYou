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

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
    }


    public IEnumerator Rumble(float intensity, float duration, float decrease)
    {
        if(Gamepad.current != null)
        {
            float currentIntensity = intensity;
            while(currentIntensity > 0)
            {
                Gamepad.current.SetMotorSpeeds(currentIntensity, currentIntensity);

                yield return new WaitForSeconds(duration / intensity);

                currentIntensity -= decrease;
            }

            Gamepad.current.SetMotorSpeeds(0, 0);
        }
    }
}
