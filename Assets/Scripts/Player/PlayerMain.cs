using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public FirstPersonController playerMovement;

    public StarterAssetsInputs playerInputs;

    public PlayerNetwork playerNetwork;

    public SpawnPlayer spawnPlayer;

    public PlayerCamera playerCamera;

    public PlayerCollider playerCollider;

    public RaycastShoot shoot;

    public ProjectileThrow preyThrow;

    private bool isHunter;

    public bool IsHunter
    {
        get { return isHunter; }

        set
        {
            isHunter = value;
            if (isHunter)
            {
                SendMessage("BecomeHunter");
            }
            else
            {
                Debug.Log(gameObject.name);
                SendMessage("BecomePrey");
            }
        }
    }

    private void Start()
    {
        // InitPlayer();
    }

    public void InitPlayer()
    {
        SendMessage("InitPlayerMain", this);
    }

    public void DeadState()
    {
        Debug.Log(gameObject.name + " is dead");
    }
}
