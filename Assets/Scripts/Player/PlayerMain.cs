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

    [SerializeField]
    private GameObject playerPartToDesactivate;

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

        GameManager.Instance.LobbyCam.SetActive(true);
        playerPartToDesactivate.SetActive(false);
        IsDead = true;
        playerInputs.playerInput.SwitchCurrentActionMap("Dead");
        GetComponent<CapsuleCollider>().enabled = false;
        if (GameManager.Instance.players.Count == 1)
        {
            GameManager.Instance.teamManager.Victory(GameManager.Instance.players[0].name);
        }

    }
}
