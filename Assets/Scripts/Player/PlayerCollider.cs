using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public Trap LastTrap;
    private PlayerMain _playerMain;

    public void InitPlayerMain(PlayerMain _PM)
    {
        _playerMain = _PM;
        _PM.playerCollider = this;
    }

    public void Collision()
    {
        _playerMain.playerNetwork.GetTouchedServerRpc();
    }

    public void TriggerTrap(Trap _trap)
    {
        int _trapIndex = 0;
        for (int i = 0; i < GameManager.Instance.Items.Count; i++)
        {
            if (GameManager.Instance.Items[i] == _trap)
            {
                _trapIndex = i;
            }
        }

        _playerMain.playerNetwork.TrapEventServerRPC(_trapIndex);
    }

    private void OnCollisionEnter(Collision collision)
    {/*
        Debug.Log(gameObject.name);
        if (collision.gameObject.tag == "Bullet" && !_playerMain.IsHunter)
        {
            Debug.Log("destroy prey");
            _playerMain.playerNetwork.GetTouchedServerRpc();
           /* Destroy(collision.gameObject);
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
            
            gameObject.SetActive(false);
        }*/
    }
}
