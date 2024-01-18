using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    [Header("Entities")]

    [SerializeField]
    Transform _preyParent;
    [SerializeField]
    Transform _hunterParent;
    [SerializeField]
    GameObject _hunter;

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
            GameManager.Instance.preys[i].transform.SetParent(_preyParent);
        }

        int randomPrey = Random.Range(0, GameManager.Instance.preys.Count);

        GameManager.Instance.preys[randomPrey].transform.SetParent(_hunterParent);
        GameManager.Instance.preys[randomPrey].layer = 3;

        GameManager.Instance.preys.RemoveAt(randomPrey);

        _hunter = GameManager.Instance.preys[randomPrey];
        _hunter.GetComponent<PlayerMain>().IsHunter = true;

    }

    public void TeamRotation()
    {
        if(GameManager.Instance.preys.Count == 0)
        {
            GameManager.Instance.preys.AddRange(GameManager.Instance.players);
        }
        int randomPrey = Random.Range(0, GameManager.Instance.preys.Count);

        GameManager.Instance.preys[randomPrey].transform.SetParent(_hunterParent);
        GameManager.Instance.preys[randomPrey].layer = 3;

        GameManager.Instance.preys.RemoveAt(randomPrey);

        _hunter.layer = 6;
        _hunter.GetComponent<PlayerMain>().IsHunter = false;
        _hunter.transform.SetParent(_preyParent);
        

        _hunter = GameManager.Instance.preys[randomPrey];
        _hunter.GetComponent<PlayerMain>().IsHunter = true;

    }
}