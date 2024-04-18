using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPrey : MonoBehaviour
{
    // ProjectileThrow ProjectileThrow;

    [SerializeField]
    private float forceMultiplier = 5f;
    private float maxForce = 10f;

    private float pressStartTime;
    private float pressDuration;
    private bool isPressed;

    public Transform lauchPoint;
    public GameObject projectil;
    public float force;

    // Mettez à jour est appelé une fois par frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPressed = true;
            pressStartTime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.Space) && isPressed)
        {
            pressDuration = Time.time - pressStartTime;
            ApplyForce();
            isPressed = false;
        }

    }

    void ApplyForce()
    {
        force = Mathf.Clamp(pressDuration * forceMultiplier, 0f, maxForce);
        GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Impulse);
        var _projectile = Instantiate(projectil, lauchPoint.position, lauchPoint.rotation);
        _projectile.GetComponent<Rigidbody>().velocity = force * lauchPoint.up;
    }

    
}
