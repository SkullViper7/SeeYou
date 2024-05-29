using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InLobbyUI : MonoBehaviour
{
    public List<GameObject> players = new();

    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button quitButton;

    [SerializeField]
    private TextMeshProUGUI relayCode;

    public void InitInLobby(InLobby _inLobby)
    {
        _inLobby.inLobbyUI = this;
        
        playButton.onClick.AddListener(_inLobby.Play);
        playButton.onClick.AddListener(Play);

        quitButton.onClick.AddListener(_inLobby.QuitLobby);
        quitButton.onClick.AddListener(QuitLobby);
    }

    public void SetPlayerInfo()
    {
        //Va set les info du joueur pour tout les joueurs, ici permet juste de mettre les infos, neccesite un autre script pour l'envoyer aux autres joueurs
    }

    public void ShowCode(string _code)
    {
        relayCode.text = "Code: " +_code;
    }

    private void QuitLobby()
    {
        //Retourner à la recherche de lobby ou au home
    }

    private void OpenSettings()
    {
        //ouvre les options
    }

    private void Lock()
    {
        //verouille sa place
    }

    private void Play()
    {
        //Si tout les joueurs sont lock, lancer le jeu
        Debug.Log("play");
    }
}
