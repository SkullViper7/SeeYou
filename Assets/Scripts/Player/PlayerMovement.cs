using UnityEngine;
using Unity.Netcode;
using Unity.Mathematics;

public class PlayerMovement : NetworkBehaviour
{
    [Header("Move the camera")]
    //public Camera cam;

    [SerializeField]
    public float SensX;
    [SerializeField]
    public float SensY;

    public Transform Orientation;

    float XRotation;
    float YRotation;

    Vector3 rotate;
    public float sensitivity = -1f;

    private Rigidbody rb;

    public Transform cameraPosition;


    [Header("Move the player")]
    public Vector3 direction;

    [SerializeField]
    float speed;

    [SerializeField]
    private float maxVelocity;

    PlayerMain _playerMain;

    protected virtual void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void FixedUpdate()
    {
        if (_playerMain != null)
        {
            if (_playerMain.playerNetwork.IsOwner)
            {
                //this.MoveCamera();

                //si il est une proie
                this.MovePlayer();
                MoveCamera();
                //transform.position = cameraPosition.position;
            }
        }
        else
        {
            this.GetComponent<PlayerMain>().InitPlayer();
        }
    }

    void Move()
    {
        this.transform.Translate(this.direction * this.speed * Time.deltaTime);

    }

    void MoveCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensY;

        YRotation += mouseX;

        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -90, 90);

        transform.rotation = Quaternion.Euler(XRotation, YRotation, 0);
        Orientation.rotation = Quaternion.Euler(0, YRotation, 0);



        /*this.SensY = Input.GetAxis("Mouse X");
        this.SensX = Input.GetAxis("Mouse Y");
        this.rotate = new Vector3(this.SensX, this.SensY * this.sensitivity, 0);
        this.transform.eulerAngles = this.transform.eulerAngles - this.rotate;*/
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        this._playerMain = _PM;
        _PM.playerMovement = this;
    }

    public void BecomeHunter()
    {
        this.direction = Vector3.zero;
    }

    public void BecomePrey()
    {
        this.direction = Vector3.zero;
    }

    void MovePlayer()
    {
        direction = Orientation.forward * direction.z + Orientation.right * direction.x;
        //transform.Translate(this.moveDirection.normalized * this.speed * Time.deltaTime);
        rb.AddForce(direction.normalized * speed * 10f, ForceMode.Force);
        if (math.abs(rb.velocity.z) > maxVelocity)
        {
            if (rb.velocity.z > 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maxVelocity);
            }
            else
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -maxVelocity);
            }
        }

        if (math.abs(rb.velocity.x) > maxVelocity)
        {
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector3(maxVelocity, rb.velocity.y, rb.velocity.z);
            }
            else
            {
                rb.velocity = new Vector3(-maxVelocity, rb.velocity.y, rb.velocity.z);
            }
        }

        //transform.Translate(moveDirection.normalized * speed * Time.deltaTime);
    }
}