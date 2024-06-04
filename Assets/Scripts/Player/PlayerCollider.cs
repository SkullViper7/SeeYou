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
        bool _hasTrap = false;
        for (int i = 0; i < GameManager.Instance.Items.Count; i++)
        {
            if (GameManager.Instance.Items[i].GetComponent<Trap>() == _trap)
            {
                Debug.Log(_trap.name);
                Debug.Log(GameManager.Instance.Items[i].name);
                _trapIndex = i;
                _hasTrap = true;
            }
        }

        if (_hasTrap)
        {
            _playerMain.playerNetwork.TrapEventServerRPC(_trapIndex);
        }
    }
}
