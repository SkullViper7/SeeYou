using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootPrey : MonoBehaviour
{
    // ProjectileThrow ProjectileThrow;

    [SerializeField]
    private float forceMultiplier = 10f;
    private float maxForce = 20f;

    private float pressStartTime;
    private float pressDuration;
    private bool isPressed;

    public Transform lauchPoint;
    public GameObject projectil;
    public float force;

    private PlayerMain main;

    // public void OnThrow(InputAction.CallbackContext value)
    // {
    //     if (value.performed)
    //     {
    //         isPressed = true;
    //         pressStartTime = Time.time;
    //         if(force > maxForce)
    //         {
    //             maxForce = force;
    //         }
    //     }
    //     if (value.canceled)
    //     {
    //         pressDuration = Time.time - pressStartTime;
    //         ApplyForce();
    //         _animationUpdater.UpdateAnimation(2);
    //         isPressed = false;
    //     }
    // }

    public void ApplyForce()
    {
        force = Mathf.Clamp(pressDuration * forceMultiplier, 0f, maxForce);
        //GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Impulse);
        var _projectile = Instantiate(projectil, lauchPoint.position, lauchPoint.rotation);
        _projectile.GetComponent<Rigidbody>().velocity = force * lauchPoint.up;
    }    

    public void InitPlayerMain(PlayerMain _PM)
    {
        // _PM.preyThrow = this;
        main = _PM;
    }
}
