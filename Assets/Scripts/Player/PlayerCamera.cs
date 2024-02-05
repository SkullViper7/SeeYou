using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera cam;

    [SerializeField]
    private LayerMask _hunterMask;

    [SerializeField]
    private LayerMask _preyMask;

    public void ActiveCam()
    {
        this.cam.gameObject.SetActive(true);
    }

    public void BecomeHunter()
    {
        this.cam.cullingMask = _hunterMask;
    }

    public void BecomePrey()
    {
        this.cam.cullingMask = _preyMask;
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        _PM.playerCamera = this;
    }
}
