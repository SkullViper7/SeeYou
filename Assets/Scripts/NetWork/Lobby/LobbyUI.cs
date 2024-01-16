using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies.Models;
using Unity.VisualScripting;


public class LobbyUI : MonoBehaviour
{
    RelayLobby relayLobby;
    [SerializeField] private Transform lobbySingleTemplate;
    [SerializeField] private Transform container;

    private void Start()
    {
        if(relayLobby == null)
        {
            relayLobby = GetComponent<RelayLobby>();
        }
        relayLobby.CreateLobby();
        relayLobby.ListLobbies();
    }
    private void UpdateLobbyList(List<Lobby> lobbyList)
    {
        foreach(Transform child in container)
        {
            if(child == lobbySingleTemplate)
            {
                foreach(Lobby lobby in lobbyList) 
                { 
                  /*  Transform lobbySingleTransform = Instantiate(lobbySingleTemplate, container);
                    lobbySingleTransform.gameObject.SetActive(true);
                    LobbyListSingleUI lobbyListSingleUI = lobbySingleTransform.GetComponent<LobbyListSingleUI>();
                    lobbyListSingleUI.UpdateLobby(lobby);*/
                }
            }
        }
    }

    public void CreateALobby()
    {
        relayLobby.CreateLobby();
    }

    public void JoinALobbby()
    {
        relayLobby.ListLobbies();
    }
}
