using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeamManager : MonoBehaviour
{
    [Header("Entities")]

    [SerializeField]
    Transform _preyParent;
    [SerializeField]
    Transform _hunterParent;
    [SerializeField]
    GameObject _hunter;
    public TMP_Text text;
    bool CanChangeHungter = true;

    private void Start()
    {
        GameManager.Instance.teamManager = this;
    }

    public int FindAHunter()
    {
        return (Random.Range(0, GameManager.Instance.preys.Count));
       // return GameManager.Instance.preys[Random.Range(0, GameManager.Instance.preys.Count)];
    }

    public void SetHunter(int hunterNetwork)
    {
        this._hunter = GameManager.Instance.preys[hunterNetwork];
        this._hunter.transform.SetParent(this._hunterParent);
        this._hunter.layer = 3;
        GameManager.Instance.preys.Remove(this._hunter);
        this._hunter.GetComponent<PlayerMain>().IsHunter = true;
        this.text.text = this._hunter.name;
    }

    public void SetPreys(GameObject newPrey)
    {
        newPrey.layer = 6;
        if (newPrey.GetComponent<PlayerMain>().IsHunter)
        {
            newPrey.GetComponent<PlayerMain>().IsHunter = false;
        }

        newPrey.transform.SetParent(this._preyParent);
    }

    public void SetAllPlayerInPrey()
    {
        //GameManager.Instance.preys.AddRange(GameManager.Instance.players);
        for (int i = 0; i < GameManager.Instance.preys.Count; i++)
        {
            SetPreys(GameManager.Instance.preys[i]);
            Debug.Log(GameManager.Instance.preys[i].name);
            //GameManager.Instance.preys[i].layer = 6;
            //GameManager.Instance.preys[i].transform.SetParent(this._preyParent);
        }
    }

    public void StartRotation(int newHunter)
    {
        Debug.Log("startrot");
        GameManager.Instance.preys = GameManager.Instance.players;

        SetAllPlayerInPrey();

        SetHunter(newHunter);

        // int randomPrey = Random.Range(0, GameManager.Instance.preys.Count);
        // GameManager.Instance.network.ChangeHunterServerRpc(GameManager.Instance.preys[randomPrey]);
        //int randomPrey = Random.Range(0, GameManager.Instance.preys.Count);

        //GameManager.Instance.preys[randomPrey].transform.SetParent(this._hunterParent);
        //GameManager.Instance.preys[randomPrey].layer = 3;
        //this._hunter = GameManager.Instance.preys[randomPrey];
        //GameManager.Instance.preys.RemoveAt(randomPrey);
        //this._hunter.GetComponent<PlayerMain>().IsHunter = true;
        //this.text.text = this._hunter.name;
    }

    public void TeamRotation(int newHunter)
    {
        Debug.Log("teamrot");
        //GameManager.Instance.network.ChangePreyServerRpc(_hunter);
        if (GameManager.Instance.preys.Count == 0)
        {
            //  GameManager.Instance.network.SetAllPlayerInPreyServerRpc();
            GameManager.Instance.preys = GameManager.Instance.players;
            SetAllPlayerInPrey();
        }

        SetPreys(this._hunter);
        SetHunter(newHunter);
        //int randomPrey = Random.Range(0, GameManager.Instance.preys.Count);
        //  GameManager.Instance.network.ChangeHunterServerRpc(GameManager.Instance.preys[randomPrey]);
        //GameManager.Instance.preys[randomPrey].transform.SetParent(this._hunterParent);
        //GameManager.Instance.preys[randomPrey].layer = 3;

        /*this._hunter.layer = 6;
        this._hunter.GetComponent<PlayerMain>().IsHunter = false;
        this._hunter.transform.SetParent(this._preyParent);
        this._hunter = GameManager.Instance.preys[randomPrey];
        GameManager.Instance.preys.RemoveAt(randomPrey);

        this._hunter.GetComponent<PlayerMain>().IsHunter = true;
        this.text.text = this._hunter.name;*/
    }

    public void SetHunterForAllClients(GameObject _hunterServer)
    {
        this._hunter = _hunterServer;
        this._hunter.transform.SetParent(this._hunterParent);
        this._hunter.layer = 3;
        this._hunter.GetComponent<PlayerMain>().IsHunter = true;
        this.text.text = this._hunter.name;
        GameManager.Instance.preys.Remove(this._hunter);
    }

    public void SetPreyForAllClients(GameObject _preyServer)
    {
        _preyServer.layer = 6;
        _preyServer.GetComponent<PlayerMain>().IsHunter = false;
        _preyServer.transform.SetParent(this._preyParent);
    }
}