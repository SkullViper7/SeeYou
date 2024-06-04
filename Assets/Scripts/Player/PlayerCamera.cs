using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera cam;

    [SerializeField]
    private GameObject cameraObject;

    [SerializeField]
    private LayerMask _hunterMask;

    [SerializeField]
    private LayerMask _preyMask;

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
}
