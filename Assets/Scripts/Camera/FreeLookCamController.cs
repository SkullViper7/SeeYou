using UnityEngine;
using UnityEngine.InputSystem;

public class FreeLookCamController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] float _rotateSpeed = 100f;

    Rigidbody _rb;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 180);
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        _rb.velocity = _moveSpeed * input.y * transform.forward + _moveSpeed * input.x * transform.right;
    }


    public void OnLook(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(-input.y, input.x, 0) * _rotateSpeed * Time.deltaTime);
    }
}
