using UnityEngine;

public class InLobby : MonoBehaviour
{
    public int maxPlayerNumber;
    public int actualPlayerNumber;

    public InLobbyUI inLobbyUI;

    public void PlayerEnterInLobby()
    {
        if (actualPlayerNumber < maxPlayerNumber)
        {
            actualPlayerNumber++;
            inLobbyUI.SetPlayerInfo();
        }
        
    }

    private void Start()
    {
        SendMessage("InitInLobby", this);
    }
}
