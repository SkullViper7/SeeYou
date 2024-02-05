using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PSGamepadRemove : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        CheckAndRemovePS4Gamepads();
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added && device.name.StartsWith("DualShock4GamepadHID"))
        {
            InputSystem.RemoveDevice(device);
        }
    }

    private void CheckAndRemovePS4Gamepads()
    {
        var connectedGamepads = InputSystem.devices.Where(device => device.name.StartsWith("DualShock4GamepadHID")).ToList();

        foreach (var gamepad in connectedGamepads)
        {
            InputSystem.RemoveDevice(gamepad);
        }
    }
}