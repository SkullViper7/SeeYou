using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPrey : MonoBehaviour
{
    [SerializeField]
    private float forceMultiplier = 5f;
    private float maxForce = 10f;

    private float pressStartTime;
    private float pressDuration;
    private bool isPressed;

    public Transform lauchPoint;
    public GameObject projectil;
    private float force;

    public LineRenderer lineRenderer;
    public int linePoints = 100;
    public float timeIntervalinPoints = 0.1f;

    // Mettez à jour est appelé une fois par frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPressed = true;
            pressStartTime = Time.time;
            lineRenderer.enabled = true;
        }

        if (Input.GetKeyUp(KeyCode.Space) && isPressed)
        {
            pressDuration = Time.time - pressStartTime;
            ApplyForce();
            isPressed = false;
            DrawTrajectory();
            
        }

    }

    void ApplyForce()
    {
        force = Mathf.Clamp(pressDuration * forceMultiplier, 0f, maxForce);
        GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Impulse);
        var _projectile = Instantiate(projectil, lauchPoint.position, lauchPoint.rotation);
        _projectile.GetComponent<Rigidbody>().velocity = force * lauchPoint.up;
    }

    void DrawTrajectory()
    {
        Vector3 origin = lauchPoint.position;
        Vector3 startVelocity = force * lauchPoint.up;
        lineRenderer.positionCount = linePoints;
        float time = 0;
        for(int i = 0; i < linePoints; i++)
        {
            var x = (startVelocity.x * time) + (Physics.gravity.x / 2 * time * time);
            var y = (startVelocity.y * time) + (Physics.gravity.y / 2 * time * time);
            Vector3 point = new Vector3(x, y, 0f);
            lineRenderer.SetPosition(i, origin + point);
            time += timeIntervalinPoints;
        }
    }
}
