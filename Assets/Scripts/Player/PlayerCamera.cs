using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera preyCam;

    public Camera hunterCam;

    [SerializeField]
    private GameObject preyCamObject;

    [SerializeField]
    private GameObject hunterCamObject;

    [SerializeField]
    private LayerMask _hunterMask;

    [SerializeField]
    private LayerMask _preyMask;

    public void ActivePreyCam()
    {
        preyCamObject.SetActive(true);
    }

    public void ActiveHunterCam()
    {
        hunterCamObject.SetActive(true);
    }

    public void BecomeHunter()
    {
        preyCam.cullingMask = _hunterMask;
    }

    public void BecomePrey()
    {
        preyCam.cullingMask = _preyMask;
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        _PM.playerCamera = this;
    }
}
