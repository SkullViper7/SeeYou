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
        public NetworkVariable<int> NumberOfPlayer = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);


        private bool pcAssigned;

        [SerializeField] TextMeshProUGUI ipAddressText;
        [SerializeField] TMP_InputField ip;

        [SerializeField] string ipAddress;

        [SerializeField] TMP_InputField numberOfPlayerField;
        [SerializeField] UnityTransport transport;

        void Start()
        {
            ipAddress = "0.0.0.0";
            SetIpAddress(); // Set the Ip to the above address
            pcAssigned = false;
            InvokeRepeating("assignPlayerController", 0.1f, 0.1f);
            numberOfPlayerField.onValueChanged.AddListener(ValidateInput);
        }

        public void StartServer()
        {
            NetworkManager.Singleton.StartServer();
            GetLocalIPAddress();
        }

        // To Host a game
        public void StartHost()
        {
            NetworkManager.Singleton.StartHost();
            GetLocalIPAddress();
            UpdateNumberOfPlayerClientRpc(NumberOfPlayer.Value);

            Debug.Log(int.Parse(numberOfPlayerField.text));
        }

        // To Join a game
        public void StartClient()
        {
            ipAddress = ip.text;
            Debug.Log(ip.text);
            SetIpAddress();
            NetworkManager.Singleton.StartClient();
            Debug.Log(NetworkManager.Singleton.StartClient());
            Invoke("LauncheCLient", 1.0f);
            
            /*if ()
            {
                NumberOfPlayer = int.Parse(numberOfPlayerField.text);
            }*/
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
                    ipAddressText.text = ip.ToString();
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

        private void ValidateInput(string input)
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
            NumberOfPlayer.Value = numberOfPlayer;
            Debug.Log("Updated NumberOfPlayer on client: " + numberOfPlayer);
        }

    }
}

