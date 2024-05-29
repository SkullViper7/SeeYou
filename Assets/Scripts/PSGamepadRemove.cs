using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PSGamepadRemove : MonoBehaviour
{
    /// <summary>
    /// This method is called when the script instance is being loaded.
    /// It is responsible for setting up the game object to persist across scenes.
    /// It also checks for and removes PlayStation 4 gamepads, and sets up the event
    /// handler for device changes.
    /// </summary>
    private void Awake()
    {
        // Set the game object to persist across scenes
        DontDestroyOnLoad(gameObject);

        // Check for and remove PlayStation 4 gamepads
        CheckAndRemovePS4Gamepads();

        // Set up the event handler for device changes
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    /// <summary>
    /// This method is called when the script instance is disabled and should be used
    /// to clean up any event handlers.
    /// </summary>
    private void OnDisable()
    {
        // Remove the event handler for device changes
        // This is important to avoid memory leaks
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    /// <summary>
    /// Event handler for device changes. Checks if a new device is added and if it is a
    /// PlayStation 4 gamepad. If it is, the gamepad is immediately removed.
    /// </summary>
    /// <param name="device">The device that was added or removed.</param>
    /// <param name="change">The type of change that occurred.</param>
    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        // Check if a device was added
        if (change == InputDeviceChange.Added)
        {
            // Check if the device is a PlayStation 4 gamepad
            if (device.name.StartsWith("DualShock4GamepadHID"))
            {
                // Remove the device
                InputSystem.RemoveDevice(device);
            }
        }
    }

    /// <summary>
    /// Checks for and removes PlayStation 4 gamepads.
    /// </summary>
    private void CheckAndRemovePS4Gamepads()
    {
        // Get all the connected gamepads
        var connectedGamepads = InputSystem.devices
            .Where(device => device.name.StartsWith("DualShock4GamepadHID"))
            .ToList();

        // Iterate over each gamepad and remove it
        foreach (var gamepad in connectedGamepads)
        {
            // Remove the device
            InputSystem.RemoveDevice(gamepad);
        }
    }
}