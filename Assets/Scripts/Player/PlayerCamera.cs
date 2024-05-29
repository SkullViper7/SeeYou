using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera cam;

    [SerializeField]
    private GameObject cameraObject;
        
    /*public float SensX;
    public float SensY;

    public Transform Orientation;

    float XRotation;
    float YRotation;*/

    //public float sensitivity = -1f;
    //private Vector3 rotate;

    [SerializeField]
    private LayerMask _hunterMask;

    [SerializeField]
    private LayerMask _preyMask;


    private void Start()
    {
        
    }


    private void Update()
    {
        /*float mouseX = Input.GetAxisRaw("Mouse SensX") * Time.deltaTime * SensX;
        float mouseY = Input.GetAxisRaw("Mouse SensY") * Time.deltaTime * SensY;

        YRotation += mouseX;

        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -90, 90);

        transform.rotation = Quaternion.Euler(XRotation, YRotation, 0);
        Orientation.rotation = Quaternion.Euler(0, YRotation, 0);*/

        //this.MoveCamera();
    }

    public void ActiveCam()
    {
        cameraObject.SetActive(true);
    }

    public void BecomeHunter()
    {
        cam.cullingMask = _hunterMask;
    }

    public void BecomePrey()
    {
        cam.cullingMask = _preyMask;
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        _PM.playerCamera = this;
    }

    /*void MoveCamera()
    {
        this.y = Input.GetAxis("Mouse SensX");
        this.x = Input.GetAxis("Mouse SensY");
        this.rotate = new Vector3(this.x, this.y * this.sensitivity, 0);
        this.transform.eulerAngles = this.transform.eulerAngles - this.rotate;
    }*/
}
