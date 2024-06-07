using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public bool IsDead;

    public FirstPersonController playerMovement;

    public StarterAssetsInputs playerInputs;

    public PlayerNetwork playerNetwork;

    public SpawnPlayer spawnPlayer;

    public PlayerCamera playerCamera;

    public PlayerCollider playerCollider;

    public Shoot shoot;

    public PlayerUI PlayerUI;

    public ProjectileThrow preyThrow;

    private bool isHunter;

    [SerializeField] GameObject _hunterMesh;
    [SerializeField] GameObject _preyMesh;

    public GameObject MeshToHide;
    public int LayerToChangeThePreyMesh;

    public bool IsHunter
    {
        get { return isHunter; }

        set
        {
            isHunter = value;
            if (isHunter)
            {
                SendMessage("BecomeHunter");
                if (playerNetwork.IsOwner)
                {
                    hunterGun.SetActive(true);
                }
                else
                {
                    preyGunView.SetActive(true);
                }

            }
            else
            {
                SendMessage("BecomePrey");
                if (playerNetwork.IsOwner)
                {
                    hunterGun.SetActive(false);
                }
                else
                {
                    preyGunView.SetActive(false);
                }
            }
        }
    }

    [SerializeField]
    private GameObject playerPartToDesactivate;

    [SerializeField]
    private GameObject hunterGun;

    [SerializeField]
    private GameObject preyGunView;

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
        for (int i = 0; i < GameManager.Instance.preys.Count; i++)
        {
            if (GameManager.Instance.preys[i] != null)
            {
                if (GameManager.Instance.preys[i] == gameObject)
                {
                    GameManager.Instance.preys.Remove(GameManager.Instance.preys[i]);
                }
            }
        }

        for (int i = 0; i < GameManager.Instance.players.Count; i++)
        {
            if (GameManager.Instance.players[i] != null)
            {
                if (GameManager.Instance.players[i] == gameObject)
                {
                    GameManager.Instance.players.Remove(GameManager.Instance.players[i]);
                }
            }
        }

        GameManager.Instance.deadPanel.SetActive(true);

        GameManager.Instance.LobbyCam.SetActive(true);
        playerPartToDesactivate.SetActive(false);
        IsDead = true;
        playerInputs.playerInput.SwitchCurrentActionMap("Dead");
        GetComponent<CapsuleCollider>().enabled = false;
        if (GameManager.Instance.players.Count == 1)
        {
            GameManager.Instance.winPanel.SetActive(true);
            GameManager.Instance.teamManager.Victory(GameManager.Instance.players[0].GetComponent<PlayerNetwork>().Pseudo);
        }
    }

    void BecomeHunter()
    {
        _hunterMesh.SetActive(true);
        _preyMesh.SetActive(false);
        if (playerNetwork.IsOwner) 
        {
            playerCamera.ActiveHunterCam();
        }
    }

    void BecomePrey()
    {
        _hunterMesh.SetActive(false);
        _preyMesh.SetActive(true);
        if (playerNetwork.IsOwner)
        {
            playerCamera.ActivePreyCam();
        }
    }
}
