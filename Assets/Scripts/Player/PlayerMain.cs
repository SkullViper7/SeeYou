using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public PlayerInputs playerInputs;

    public PlayerNetwork playerNetwork;

    public SpawnPlayer spawnPlayer;

    public PlayerCamera playerCamera;

    public Shoot shoot;

    private bool isHunter;

    public bool IsHunter
    {
        get { return this.isHunter; }

        set
        {
            this.isHunter = value;
            if (this.isHunter)
            {
                this.gameObject.SendMessage("BecomeHunter");
            }
            else
            {
                this.gameObject.SendMessage("BecomePrey");
            }
        }
    }

    private void Start()
    {
        // InitPlayer();
    }

    public void InitPlayer()
    {
        this.gameObject.SendMessage("InitPlayerMain", this);
    }
}
