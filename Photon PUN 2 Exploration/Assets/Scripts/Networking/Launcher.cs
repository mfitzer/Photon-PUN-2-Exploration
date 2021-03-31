using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace mfitzer.Networking
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        private const string gameVersion = "1";

        [SerializeField]
        private byte maxPlayersPerRoom = 4;

        [SerializeField]
        private GameObject controlPanel;

        [SerializeField]
        private GameObject keyboardInput;

        [SerializeField]
        private TextMeshProUGUI connectionProgressLabel;

        private bool isConnecting = false;

        [SerializeField, Tooltip("Build index of the scene to load after this scene.")]
        private int nextSceneIndex = 1;

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            controlUI(false);
        }

        private void controlUI(bool connecting)
        {
            controlPanel.SetActive(!connecting);
            keyboardInput.SetActive(!connecting);
            connectionProgressLabel.gameObject.SetActive(connecting);
        }

        public void connect()
        {
            controlUI(true);

            if (PhotonNetwork.IsConnected) //Already connected 
            {
                PhotonNetwork.JoinRandomRoom(); //Try joining a random room

                connectionProgressLabel.text = "Connected to Photon server.\nJoining random room.";
            }
            else //Not connected, connect to Photon server
            {
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;

                connectionProgressLabel.text = "Connecting to Photon server.";
            }
        }

        public override void OnConnectedToMaster()
        {
            connectionProgressLabel.text += "\nConnected to Photon server";

            //This prevents a room from being rejoined when this callback is called after the user leaves a room and returns to the lobby
            if (isConnecting)
            {
                PhotonNetwork.JoinRandomRoom();
                connectionProgressLabel.text += "\nJoining random room.";
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            connectionProgressLabel.text += "\nDisconnected from PUN. Reason: " + cause;
            Debug.LogWarningFormat("Disconnected from PUN. Reason {0}", cause);
            controlUI(false);
            isConnecting = false;
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            connectionProgressLabel.text += "\nFailed to join random room, creating a room.";

            PhotonNetwork.CreateRoom(null,
                new RoomOptions { 
                    MaxPlayers = maxPlayersPerRoom
                }
            );
        }

        public override void OnJoinedRoom()
        {
            connectionProgressLabel.text += "\nJoined room.";

            //Load next scene in build settings
            PhotonNetwork.LoadLevel(nextSceneIndex);
        }
    }
}
