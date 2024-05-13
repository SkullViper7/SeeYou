using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private PlayerMain _playerMain;

    public void InitPlayerMain(PlayerMain _PM)
    {
        _playerMain = _PM;
        _PM.playerCollider = this;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("destroy prey");
        }
    }
}
