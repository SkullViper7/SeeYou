using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TrajectoryPredictor))]
public class ProjectileThrow : MonoBehaviour
{
    TrajectoryPredictor trajectoryPredictor;

    public GameObject objectToThrow;

    [SerializeField, Range(0.0f, 50.0f)]
    public float force;

    [SerializeField]
    Transform StartPosition;

    public InputAction fire;

    private PlayerMain main;

    void Start()
    {
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();
    }

    public void GetAnotherItem(GameObject _objectToThrow) 
    {
        objectToThrow = _objectToThrow;
    }
     public void Predict()
    {
        trajectoryPredictor.PredictTrajectory(ProjectileData());
    }

    ProjectileProperties ProjectileData()
    {
        ProjectileProperties properties = new ProjectileProperties();
        Rigidbody r = objectToThrow.GetComponent<Rigidbody>();

        properties.direction = StartPosition.forward;
        properties.initialPosition = StartPosition.position;
        properties.initialSpeed = force;
        properties.mass = r.mass;
        properties.drag = r.drag;

        return properties;
    }

    public void ThrowObject()
    {
        GameObject thrownObject = Instantiate(objectToThrow, StartPosition.position, Quaternion.identity);
        thrownObject.GetComponent<Rigidbody>().AddForce(StartPosition.forward * force, ForceMode.Impulse);
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        _PM.preyThrow = this;
        main = _PM;
    }

}