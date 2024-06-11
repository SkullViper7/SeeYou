using Unity.Netcode;
using UnityEngine;

public class InitNetwork : MonoBehaviour
{
    [SerializeField]
    private GameObject networkManagerObject;

    private NetworkUI networkUI;
    private void Awake()
    {
        if (NetworkManager.Singleton == null)
        {
            networkManagerObject.SetActive(true);
        }
        /*else
        {
            NetworkManager.Singleton.Shutdown();
            Destroy(NetworkManager.Singleton.gameObject);
            networkManagerObject.SetActive(true);
        }*/

        networkUI = GetComponent<NetworkUI>();
        networkUI.enabled = true;
    }
}
