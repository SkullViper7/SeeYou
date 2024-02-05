using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    [Header("Move the camera")]
    private float x;
    private float y;
    public float sensitivity = -1f;
    private Vector3 rotate;

    [Header("Move the player")]
    public Vector3 direction;
    float speed;

    PlayerMain _playerMain;

    protected virtual void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        this.speed = 5;
    }

    protected virtual void FixedUpdate()
    {
        if (_playerMain != null)
        {
            if (_playerMain.playerNetwork.IsOwner)
            {
                this.MoveCamera();

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
        this.y = Input.GetAxis("Mouse X");
        this.x = Input.GetAxis("Mouse Y");
        this.rotate = new Vector3(this.x, this.y * this.sensitivity, 0);
        this.transform.eulerAngles = this.transform.eulerAngles - this.rotate;
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