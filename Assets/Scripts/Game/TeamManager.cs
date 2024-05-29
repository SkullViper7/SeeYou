using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class TeamManager : MonoBehaviour
{
    [Header("Entities")]

    [SerializeField]
    Transform _preyParent;
    [SerializeField]
    Transform _hunterParent;
    [SerializeField]
    public GameObject _hunter;
    public TMP_Text text;
    bool CanChangeHunter = true;

    private bool hasAWinner;

    private void Start()
    {
        GameManager.Instance.teamManager = this;
    }

    public int FindAHunter()
    {
        if (GameManager.Instance.preys.Count == 0)
        {
            //  GameManager.Instance.network.SetAllPlayerInPreyServerRpc();
            GameManager.Instance.preys.AddRange(GameManager.Instance.players);
            SetAllPlayerInPrey();
        }

        int randomPrey = Random.Range(0, GameManager.Instance.preys.Count);
        return randomPrey;
       // return GameManager.Instance.preys[Random.Range(0, GameManager.Instance.preys.Count)];
    }

    public void SetHunter(int hunterNetwork)
    {
        _hunter = GameManager.Instance.preys[hunterNetwork];
        _hunter.transform.SetParent(this._hunterParent);
        _hunter.layer = 3;
        _hunter.tag = "Player";
        GameManager.Instance.preys.Remove(this._hunter);
        _hunter.GetComponent<PlayerMain>().IsHunter = true;
        text.text = this._hunter.name;
    }

    public void SetPreys(GameObject newPrey)
    {
        newPrey.layer = 6;
        newPrey.tag = "Prey";
        if (newPrey.GetComponent<PlayerMain>().IsHunter)
        {
            newPrey.GetComponent<PlayerMain>().IsHunter = false;
        }

        newPrey.transform.SetParent(_preyParent);
    }

    public void SetAllPlayerInPrey()
    {
        for (int i = 0; i < GameManager.Instance.preys.Count; i++)
        {
            SetPreys(GameManager.Instance.preys[i]);
        }
    }

    public void StartRotation()
    {
        SetAllPlayerInPrey();
    }

    public void TeamRotation()
    {
        if (!hasAWinner)
        {
            if (GameManager.Instance.preys.Count == 0)
            {
                GameManager.Instance.preys.AddRange(GameManager.Instance.players);
                SetAllPlayerInPrey();
            }
            else
            {
                SetPreys(_hunter);
            }
            
            //SetHunter(Random.Range(0, GameManager.Instance.preys.Count));
        }
    }

    public void SetHunterForAllClients(int _hunterServer)
    {
        if (!hasAWinner)
        {
            _hunter = GameManager.Instance.preys[_hunterServer];
            GameManager.Instance.preys.RemoveAt(_hunterServer);

            if (GameManager.Instance.preys.Count == 0)
            {
                GameManager.Instance.preys.AddRange(GameManager.Instance.players);
                SetAllPlayerInPrey();
            }
        
            _hunter.transform.SetParent(_hunterParent);
            _hunter.layer = 3;
            _hunter.tag = "Player";
            _hunter.GetComponent<PlayerMain>().IsHunter = true;
            text.text = _hunter.name;
        }
    }

    public void SetPreyForAllClients(int _preyServer)
    {
        if (GameManager.Instance.preys.Count == 0)
        {
            GameManager.Instance.preys.AddRange(GameManager.Instance.players);

            SetAllPlayerInPrey();
            GameObject _prey = GameManager.Instance.preys[_preyServer];
            _prey.layer = 6;
            _prey.tag = "Prey";
            _prey.GetComponent<PlayerMain>().IsHunter = false;
            _prey.transform.SetParent(_preyParent);
        }
    }

    public void ManagerSet(int _hunter)
    {
        this._hunter = GameManager.Instance.preys[_hunter];
        GameManager.Instance.preys.Remove(GameManager.Instance.preys[_hunter]);
    }

    public int FindAHunterServ()
    {
        int randomPrey = Random.Range(0, GameManager.Instance.preys.Count);
        return randomPrey;
    }

    public void Victory(string _winnerName)
    {
        hasAWinner = true;
        text.text = _winnerName + " Win !";
    }
}