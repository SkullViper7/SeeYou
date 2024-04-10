using UnityEngine;
using UnityEngine.InputSystem;

public class FreeLookCamController : MonoBehaviour
{
    [SerializeField]
    float _sensitivity = 2f;
    [SerializeField]
    float _moveSpeed = 5f;

    Vector2 _lookRotation;
    Vector2 _velocity;

    public void OnLook(InputAction.CallbackContext value)
    {
        _lookRotation = value.ReadValue<Vector2>();

        transform.rotation = Quaternion.Euler(_lookRotation * _sensitivity);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        Vector3 deltaPosition = Vector3.zero;

        _velocity = value.ReadValue<Vector2>();

        if (_velocity.x > 0)
        {
            deltaPosition += transform.forward;
        }
        if (_velocity.x < 0)
        {
            deltaPosition -= transform.forward;
        }
        if (_velocity.y > 0)
        {
            deltaPosition += transform.right;
        }
        if (_velocity.y < 0)
        {
            deltaPosition -= transform.right;
        }

        transform.position += deltaPosition * _moveSpeed;
    }
}
