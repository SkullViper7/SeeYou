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
    public GameObject _hunter;
    public TMP_Text text;
    bool CanChangeHunter = true;

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
            //GameManager.Instance.preys[i].layer = 6;
            //GameManager.Instance.preys[i].transform.SetParent(this._preyParent);
        }
    }

    public void StartRotation()
    {
        Debug.Log("startrot");

        SetAllPlayerInPrey();
        //SetHunter(Random.Range(0, GameManager.Instance.preys.Count));

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

    public void TeamRotation()
    {
        Debug.Log("teamrot");
        //GameManager.Instance.network.ChangePreyServerRpc(_hunter);
        if (GameManager.Instance.preys.Count == 0)
        {
            //  GameManager.Instance.network.SetAllPlayerInPreyServerRpc();
            GameManager.Instance.preys.AddRange(GameManager.Instance.players);
            SetAllPlayerInPrey();
        }

        SetPreys(this._hunter);
        SetHunter(Random.Range(0, GameManager.Instance.preys.Count));
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

    public void SetHunterForAllClients(int _hunterServer)
    {
        if (GameManager.Instance.preys.Count == 0)
        {
            //  GameManager.Instance.network.SetAllPlayerInPreyServerRpc();
            GameManager.Instance.preys.AddRange(GameManager.Instance.players);
            SetAllPlayerInPrey();
        }

        this._hunter = GameManager.Instance.preys[_hunterServer];
        GameManager.Instance.preys.RemoveAt(_hunterServer);
        this._hunter.transform.SetParent(this._hunterParent);
        this._hunter.layer = 3;
        this._hunter.GetComponent<PlayerMain>().IsHunter = true;
        this.text.text = this._hunter.name;
    }

    public void SetPreyForAllClients(int _preyServer)
    {
        if (GameManager.Instance.preys.Count == 0)
        {
            foreach (GameObject player in GameManager.Instance.players)
            {
                GameManager.Instance.preys.AddRange(GameManager.Instance.players);
            }

            SetAllPlayerInPrey();
            GameObject _prey = GameManager.Instance.preys[_preyServer];
            _prey.layer = 6;
            _prey.GetComponent<PlayerMain>().IsHunter = false;
            _prey.transform.SetParent(this._preyParent);
        }
    }

    public void ManagerSet(int _hunter)
    {
        this._hunter = GameManager.Instance.preys[_hunter];
        GameManager.Instance.preys.Remove(GameManager.Instance.preys[_hunter]);
    }

    public int FindAHunterServ()
    {
       /* if (GameManager.Instance.preys.Count == 0)
        {
            //  GameManager.Instance.network.SetAllPlayerInPreyServerRpc();
            GameManager.Instance.preys.AddRange(GameManager.Instance.players);
            SetAllPlayerInPrey();
        }*/

        int randomPrey = Random.Range(0, GameManager.Instance.preys.Count);
        _hunter = GameManager.Instance.preys[randomPrey];
        return randomPrey;
    }
}