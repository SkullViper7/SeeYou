using UnityEngine;
using Unity.Netcode.Transports.UTP;
using TMPro;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
namespace Unity.Netcode.Samples
{

    public class NetworkLan : MonoBehaviour
    {
        public PreyInput preyInput;
        public NetworkVariable<int> NumberOfPlayer = new NetworkVariable<int>();
        //public GameObject ItemsToSpawn;

        public string PseudoChoosen;

        private bool pcAssigned;

        //public TextMeshProUGUI PlayerNeeded;

        //[SerializeField] TextMeshProUGUI ipAddressText;
        //[SerializeField] TMP_InputField ip;

        [SerializeField] string ipAddress;

        //private TMP_InputField numberOfPlayerField;

        //private GameObject clientLobby;

        //private GameObject hostLobby;

        //private GameObject lobby;

        //private TMP_InputField pseudoField;

        [SerializeField]
        private UnityTransport transport;

        //[SerializeField] Button _joinButton;
        //[SerializeField] Button _createButton;

        bool _hasSetName = false;
        bool _hasSetNumber = false;

        public NetworkUI networkUI;

        private void Start()
        {
            networkUI = GameManager.Instance.GetComponent<NetworkUI>();
            StartTheNetworkLan();
        }

        void StartTheNetworkLan()
        {
            ipAddress = "0.0.0.0";
            SetIpAddress(); // Set the Ip to the above address
            pcAssigned = false;
            InvokeRepeating("assignPlayerController", 0.1f, 0.1f);
            networkUI.numberOfPlayerField.onValueChanged.AddListener(ValidateNumberInput);
            networkUI.pseudoField.onValueChanged.AddListener(ValidatePseudoInput);
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            if (PlayerPrefs.GetString("Pseudo") != null)
            {
                networkUI.pseudoField.placeholder.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Pseudo");
                networkUI._joinButton.interactable = true;
            }

        }

        private void InitUI(NetworkUI _networkUI)
        {
            networkUI = _networkUI;
            StartTheNetworkLan();
            networkUI._createButton.onClick.AddListener(StartHost);
            networkUI._joinButton.onClick.AddListener(StartClient);
        }

        public void StartServer()
        {
            NetworkManager.Singleton.StartServer();
            GetLocalIPAddress();
        }

        // To Host a game
        public void StartHost()
        {
            UpdateNumberOfPlayerClientRpc(NumberOfPlayer.Value);
            UpdatePseudoOfPlayerClientRpc(PseudoChoosen);
            if (ValidateHost())
            {
                GetLocalIPAddress();
                networkUI.hostLobby.SetActive(false);
                networkUI.pseudoField.gameObject.SetActive(false);
                networkUI.numberOfPlayerField.gameObject.SetActive(false);
                NetworkManager.Singleton.StartHost();
            }
        }

        // To Join a game
        public void StartClient()
        {
            
            UpdatePseudoOfPlayerClientRpc(PseudoChoosen);
            if (ValidateClient()) 
            {
                ipAddress = networkUI.ip.text;
                SetIpAddress();
                NetworkManager.Singleton.StartClient();
            }
        }

        void OnClientConnected(ulong clientId)
        {
            Debug.Log($"Successfully connected to server with client ID: {clientId}");
            networkUI.clientLobby.SetActive(false);
            networkUI.pseudoField.gameObject.SetActive(false);
            Invoke("LauncheCLient", 1.0f);
            // Ajoute toute logique supplémentaire après la connexion réussie
        }

        /* Gets the Ip Address of your connected network and
        shows on the screen in order to let other players join
        by inputing that Ip in the input field */
        // ONLY FOR HOST SIDE 
        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    networkUI.ipAddressText.text = "IP : " + ip.ToString();
                    ipAddress = ip.ToString();
                    return ip.ToString();
                }
            }
            throw new System.Exception("No network adapters with an IPv4 address in the system!");
        }

        /* Sets the Ip Address of the Connection Data in Unity Transport
        to the Ip Address which was input in the Input Field */
        // ONLY FOR CLIENT SIDE
        public void SetIpAddress()
        {
            transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            if (transport != null)
            {
                transport = GetComponent<UnityTransport>();
            }

            transport.ConnectionData.Address = ipAddress;
        }

        // Assigns the player to this script when player is loaded
        private void assignPlayerController()
        {
            if (preyInput == null)
            {
                preyInput = FindObjectOfType<PreyInput>();
            }
            else if (preyInput == FindObjectOfType<PreyInput>())
            {
                pcAssigned = true;
                CancelInvoke();
            }
        }

        private void ValidateNumberInput(string input)
        {
            int value;
            if (int.TryParse(input, out value))
            {
                NumberOfPlayer.Value = int.Parse(input);
                _hasSetNumber = true;
            }
            else
            {
                networkUI.numberOfPlayerField.text = "";
                _hasSetNumber = false;
            }

            CheckHostPrerequisites();
        }

        private void ValidatePseudoInput(string input)
        {
            PseudoChoosen = input;
            _hasSetName = true;

            if (PseudoChoosen == "" && PlayerPrefs.GetString("Pseudo") == null)
            {
                _hasSetName = false;
            }

            CheckHostPrerequisites();
            CheckClientPrerequisites();
        }

        void CheckHostPrerequisites()
        {
            if ((_hasSetName && _hasSetNumber) || (PlayerPrefs.GetString("Pseudo") != null && _hasSetNumber))
            {
                networkUI._createButton.interactable = true;
            }

            else 
            {   
                networkUI._createButton.interactable = false;
            }

        }

        void CheckClientPrerequisites()
        {
            if (_hasSetName)
            {
                networkUI._joinButton.interactable = true;
            }

            else
            {
                networkUI._joinButton.interactable = false;
            }
        }

        private bool ValidateHost()
        {
            bool isValidate = false;
            if (NumberOfPlayer.Value != 0 && NumberOfPlayer.Value != 1) 
            { 
                if (ValidateClient()) 
                {
                    isValidate = true;
                }
            }

            return isValidate;
        }

        private bool ValidateClient() 
        {
            bool isValidate = false;
            if (PlayerPrefs.GetString("Pseudo") != null) 
            {
                isValidate = true;
            }

            return isValidate;
        }

        private void LauncheCLient()
        {
            RequestNumberOfPlayerServerRpc();
        }

        [ServerRpc(RequireOwnership = false)]
        private void RequestNumberOfPlayerServerRpc(ServerRpcParams rpcParams = default)
        {
            // Répondre au client avec le nombre de joueurs
            RespondNumberOfPlayerClientRpc(NumberOfPlayer.Value, rpcParams.Receive.SenderClientId);
        }

        // RPC pour envoyer le nombre de joueurs au client demandeur
        [ClientRpc]
        private void RespondNumberOfPlayerClientRpc(int numberOfPlayer, ulong clientId)
        {
            if (NetworkManager.Singleton.LocalClientId == clientId)
            {
                NumberOfPlayer.Value = numberOfPlayer;
                Debug.Log("Received NumberOfPlayer from host: " + numberOfPlayer);
            }
        }

        [ClientRpc]
        private void UpdateNumberOfPlayerClientRpc(int numberOfPlayer)
        {
            if (numberOfPlayer == 0)
            {
                numberOfPlayer = 5;
            }

            NumberOfPlayer.Value = numberOfPlayer;
        }

        [ClientRpc]
        private void UpdatePseudoOfPlayerClientRpc(string pseudoOfPlayer)
        {
            if (pseudoOfPlayer != null || PlayerPrefs.GetString("Pseudo")!= pseudoOfPlayer) 
            {
                PlayerPrefs.SetString("Pseudo", pseudoOfPlayer);
            }
        }
    }
}

