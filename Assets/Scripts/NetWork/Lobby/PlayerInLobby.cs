using System.Diagnostics;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerInLobby : NetworkBehaviour
{
    public string playerName;
    public bool isLocked;
    public bool isLobbyOwner;
    private InLobby inLobby;
    //insert Skin here
    public override void OnNetworkSpawn()
    {
        inLobby = FindObjectOfType<InLobby>();
        UnityEngine.Debug.Log(inLobby);
        inLobby.OnPlay += Play;
    }

    public void Play()
    {
        PlayServerRpc();
    }

    [ServerRpc]
    private void PlayServerRpc()
    {
        PlayClientRpc();
    }

    [ClientRpc]
    private void PlayClientRpc()
    {
        SceneManager.LoadScene(inLobby.levelToLoad);
    }
}
