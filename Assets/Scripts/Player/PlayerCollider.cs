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
            GameObject _trapToCompar = GameManager.Instance.Items[i];
            if (_trapToCompar.GetComponent<Trap>() == null) 
            {
                _trapToCompar = GameManager.Instance.Items[i].transform.GetChild(0).gameObject;
            }

            if (_trapToCompar == _trap.gameObject)
            {
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
