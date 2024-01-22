using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    public static GameManager Instance => _instance;

    public TeamManager teamManager;

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

    public void StartTheGame()
    {
        this.teamManager.StartRotation();
    }

    public void ChangeRoles()
    {
        Debug.Log("roles");
        this.StartCoroutine(this.WaitBeforeChangeRoles());
    }

    private IEnumerator WaitBeforeChangeRoles()
    {
        yield return new WaitForSeconds(3f);
        this.teamManager.TeamRotation();
    }
}