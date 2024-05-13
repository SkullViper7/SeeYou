using System.Collections.Generic;
using UnityEngine;

public class InLobbyUI : MonoBehaviour
{
    public List<GameObject> players = new();

    public void InitInLobby(InLobby _inLobby)
    {
        _inLobby.inLobbyUI = this;
    }

    public void SetPlayerInfo()
    {
        //Va set les info du joueur pour tout les joueurs, ici permet juste de mettre les infos, neccesite un autre script pour l'envoyer aux autres joueurs
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
    }
}
