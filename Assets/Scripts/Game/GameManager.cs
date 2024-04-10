using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    public static GameManager Instance => _instance;

    public TeamManager teamManager;
    public NetworkMessageManager network;
    public PlayerNetwork lastHunter;

    [HideInInspector]
    public List<GameObject> players = new();
    [HideInInspector]
    public List<GameObject> preys = new();

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

    public void ChangeRoles(int newHunter)
    {
        Debug.Log("roles");
        this.StartCoroutine(this.WaitBeforeChangeRoles(newHunter));
    }

    public IEnumerator WaitBeforeChangeRoles(int newHunter)
    {
        yield return new WaitForSeconds(1f);
        this.teamManager.TeamRotation(newHunter);
    }

    public IEnumerator WaitBeforeBegin(int newHunter)
    {
        yield return new WaitForSeconds(1f);
        this.teamManager.StartRotation(newHunter);
    }
}