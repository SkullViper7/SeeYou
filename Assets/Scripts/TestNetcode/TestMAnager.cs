using System.Collections.Generic;
using UnityEngine;

public class TestMAnager : MonoBehaviour
{
    private static TestMAnager _instance = null;
    public static TestMAnager Instance => _instance;

    public int players;

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

        DontDestroyOnLoad(gameObject);
    }
}
