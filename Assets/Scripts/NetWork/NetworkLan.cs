using UnityEngine;
using Unity.Netcode.Transports.UTP;
using TMPro;
using System.Net;
using System.Net.Sockets;
namespace Unity.Netcode.Samples
{

    public class NetworkLan : MonoBehaviour
    {
        public PreyInput preyInput;
        public NetworkVariable<int> NumberOfPlayer = new NetworkVariable<int>();
        public GameObject ItemsToSpawn;

        public string PseudoChoosen;

        private bool pcAssigned;

        public TextMeshProUGUI PlayerNeeded;

        [SerializeField] TextMeshProUGUI ipAddressText;
        [SerializeField] TMP_InputField ip;

        [SerializeField] string ipAddress;

        [SerializeField] 
        private TMP_InputField numberOfPlayerField;

        [SerializeField] 
        private TMP_InputField pseudoField;

        [SerializeField] 
        private UnityTransport transport;

        void Start()
        {
            ipAddress = "0.0.0.0";
            SetIpAddress(); // Set the Ip to the above address
            pcAssigned = false;
            InvokeRepeating("assignPlayerController", 0.1f, 0.1f);
            numberOfPlayerField.onValueChanged.AddListener(ValidateNumberInput);
            pseudoField.onValueChanged.AddListener(ValidatePseudoInput);
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
            NetworkManager.Singleton.StartHost();
            GetLocalIPAddress();
        }

        // To Join a game
        public void StartClient()
        {
            UpdatePseudoOfPlayerClientRpc(PseudoChoosen);
            ipAddress = ip.text;
            SetIpAddress();
            NetworkManager.Singleton.StartClient();
            Invoke("LauncheCLient", 1.0f);
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
                    ipAddressText.text = "IP : " + ip.ToString();
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
            }
            else
            {
                numberOfPlayerField.text = "";
            }
        }

        private void ValidatePseudoInput(string input)
        {
            PseudoChoosen = input;
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
            if (PlayerPrefs.GetString("Pseudo") == null || PlayerPrefs.GetString("Pseudo")!= pseudoOfPlayer) 
            {
                PlayerPrefs.SetString("Pseudo", pseudoOfPlayer);
            }
        }
    }
}

