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

    private void Start()
    {
        GameManager.Instance.teamManager = this;
    }

    public void StartRotation()
    {
        GameManager.Instance.preys = GameManager.Instance.players;

        for (int i = 0; i < GameManager.Instance.preys.Count; i++)
        {
            GameManager.Instance.preys[i].layer = 6;
            GameManager.Instance.preys[i].transform.SetParent(this._preyParent);
        }

        int randomPrey = Random.Range(0, GameManager.Instance.preys.Count);

        GameManager.Instance.preys[randomPrey].transform.SetParent(this._hunterParent);
        GameManager.Instance.preys[randomPrey].layer = 3;
        this._hunter = GameManager.Instance.preys[randomPrey];
        GameManager.Instance.preys.RemoveAt(randomPrey);
        this._hunter.GetComponent<PlayerMain>().IsHunter = true;
        this.text.text = this._hunter.name;
    }

    public void TeamRotation()
    {
        if (GameManager.Instance.preys.Count == 0)
        {
            GameManager.Instance.preys.AddRange(GameManager.Instance.players);
        }

        int randomPrey = Random.Range(0, GameManager.Instance.preys.Count);

        GameManager.Instance.preys[randomPrey].transform.SetParent(this._hunterParent);
        GameManager.Instance.preys[randomPrey].layer = 3;

        this._hunter.layer = 6;
        this._hunter.GetComponent<PlayerMain>().IsHunter = false;
        this._hunter.transform.SetParent(this._preyParent);
        this._hunter = GameManager.Instance.preys[randomPrey];
        GameManager.Instance.preys.RemoveAt(randomPrey);

        this._hunter.GetComponent<PlayerMain>().IsHunter = true;
        this.text.text = this._hunter.name;
    }
}