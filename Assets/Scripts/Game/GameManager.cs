using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    public static GameManager Instance => _instance;

    public TeamManager teamManager;
    public NetworkMessageManager network;
    public PlayerNetwork lastHunter;
    public GameObject LobbyCam;
    public GameObject PauseManager;
    public GameObject UiTransitionHunter;
    public GameObject UiTransitionPrey;

    public GameObject winPanel;
    public GameObject deadPanel;
    public GameObject ChronoUI;
    public GameObject HunterUIPicture;
    public TextMeshProUGUI KillUI;

    public bool GameIsStarted;

    //[HideInInspector]
    public List<GameObject> players = new();
    //[HideInInspector]
    public List<GameObject> preys = new();

    public List<GameObject> Items = new();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    /*public void StartTheGame()
    {
        this.teamManager.StartRotation();
    }*/

    public void ChangeRoles()
    {
        Debug.Log("roles");
        //this.StartCoroutine(this.WaitBeforeChangeRoles());
    }

    public IEnumerator WaitBeforeChangeRoles(int hunter)
    {
        Debug.Log("wait");
        yield return new WaitForSeconds(1f);
        this.teamManager.SetHunter(hunter);
        this.teamManager.TeamRotation();
    }

    public IEnumerator WaitBeforeBegin(int hunter)
    {
        yield return new WaitForSeconds(1f);
        this.teamManager.SetHunter(hunter);
        this.teamManager.StartRotation();
    }
}