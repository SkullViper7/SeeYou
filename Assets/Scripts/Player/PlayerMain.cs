using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public PlayerInputs playerInputs;

    public PlayerNetwork playerNetwork;

    public SpawnPlayer spawnPlayer;

    public Shoot shoot;

    public Camera cam;

    private bool isHunter;
    public bool IsHunter
    {
        get { return isHunter; }
        set
        {
            isHunter = value;
            if (isHunter)
            {
                //BecomeHunter();
                gameObject.SendMessage("BecomeHunter");
            }
            else
            {
                //BecomePrey();
                gameObject.SendMessage("BecomePrey");
            }
        }
    }

    private void Start()
    {
       // InitPlayer();
    }

    public void InitPlayer()
    {
        gameObject.SendMessage("InitPlayerMain", this);
    }

    public void ActiveCam()
    {
        cam.gameObject.SetActive(true);
    }
}
