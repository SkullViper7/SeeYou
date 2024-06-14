using UnityEngine;
using UnityEngine.VFX;

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

    [SerializeField] VisualEffect _smoke;
    [SerializeField] GameObject _smokePrefab;

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

        if (playerNetwork.IsOwner) 
        {
            GameManager.Instance.deadPanel.SetActive(true);
            GameManager.Instance.LobbyCam.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            GameManager.Instance.ChronoUI.SetActive(false);
        }
        else
        {
            GameManager.Instance.KillUI.gameObject.SetActive(true);
            GameManager.Instance.KillUI.text = playerNetwork.Pseudo + " is dead";
            Invoke("DesactivateKillUI", 2);
        }

        playerPartToDesactivate.SetActive(false);
        _smokePrefab.SetActive(true);
        _smoke.Play();
        IsDead = true;
        playerInputs.playerInput.SwitchCurrentActionMap("Dead");
        GetComponent<CapsuleCollider>().enabled = false;
        if (GameManager.Instance.players.Count == 1)
        {
            if (GameManager.Instance.players[0].GetComponent<PlayerNetwork>().IsOwner) 
            {
                GameManager.Instance.winPanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                GameManager.Instance.ChronoUI.SetActive(false);
                GameManager.Instance.players[0].GetComponent<StarterAssetsInputs>().playerInput.SwitchCurrentActionMap("Dead");
            }
            
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

    private void DesactivateKillUI() 
    {
        GameManager.Instance.KillUI.gameObject.SetActive(false);
    }
}
