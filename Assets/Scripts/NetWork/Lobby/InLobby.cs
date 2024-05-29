using UnityEngine;
using Unity.Netcode;
using System;

public class InLobby : NetworkBehaviour
{
    public int maxPlayerNumber;
    public int actualPlayerNumber;
    public InLobbyUI inLobbyUI;
    public RelayLobby RelayLobby;
    public string levelToLoad;
    public event Action OnPlay;

    [SerializeField]
    private string searchLobbySceneName;

    public void PlayerEnterInLobby()
    {
        if (actualPlayerNumber < maxPlayerNumber)
        {
            actualPlayerNumber++;
            inLobbyUI.SetPlayerInfo();
        }
        
    }

    public void Play()
    {
        OnPlay?.Invoke();
    }



    public void QuitLobby()
    {
        //SceneManager.LoadScene(searchLobbySceneName);
        RelayLobby.QuitRelay();
    }

    private void Start()
    {
        SendMessage("InitInLobby", this);
    }
}
