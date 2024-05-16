using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Lobbies.Models;


public class LobbyUI : MonoBehaviour
{
    private RelayLobby relayLobby;

    public TextMeshProUGUI relayCode;

    [SerializeField] 
    private GameObject inLobbyObject;

    [SerializeField] 
    private GameObject LobbyUIToDesactivate;

    [SerializeField] 
    private GameObject LobbyUIToActivate;

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
        relayLobby.CreateRelay();
    }

    public void JoinALobbby()
    {
        relayLobby.JoinRelay();
    }

    public void LobbyCreated(string _code)
    {
        EnterInLobby();
        inLobbyObject.SendMessage("ShowCode", _code);
    }

    public void LobbyJoined()
    {
        EnterInLobby();
        inLobbyObject.SendMessage("ShowCode", relayCode.text);
    }

    private void EnterInLobby()
    {
        inLobbyObject.SetActive(true);
        LobbyUIToDesactivate.SetActive(false);
        LobbyUIToActivate.SetActive(true);
        inLobbyObject.GetComponent<InLobby>().RelayLobby = relayLobby;
    }
}
