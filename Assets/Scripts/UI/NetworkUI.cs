using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;

public class NetworkUI : MonoBehaviour
{
    public GameObject ItemsToSpawn;

    public TextMeshProUGUI PlayerNeeded;

    public TextMeshProUGUI ipAddressText;
    public TMP_InputField ip;

    public string ipAddress;

    public TMP_InputField numberOfPlayerField;

    public GameObject clientLobby;

    public GameObject hostLobby;

    public GameObject lobby;

    public TMP_InputField pseudoField;

    public Button _joinButton;
    public Button _createButton;

    void Start()
    {
        SearchNetworkManager();
    }

    private async void WaitAwakeFromNetworkManager()
    {
        await Task.Delay(1000);
        SearchNetworkManager();
    }

    private void SearchNetworkManager()
    {
        NetworkManager.Singleton.gameObject.SendMessage("InitUI", this);
    }
}
