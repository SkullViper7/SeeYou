using UnityEngine;
using Unity.Netcode;
using UnityEngine.UIElements;

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


    [Header("Move the player")]
    public Vector3 direction;
    float speed;

    PlayerMain _playerMain;

    protected virtual void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        this.speed = 5;
    }

    protected virtual void FixedUpdate()
    {
        if (_playerMain != null)
        {
            if (_playerMain.playerNetwork.IsOwner)
            {
                //this.MoveCamera();

                //si il est une proie
                this.Move();
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
}