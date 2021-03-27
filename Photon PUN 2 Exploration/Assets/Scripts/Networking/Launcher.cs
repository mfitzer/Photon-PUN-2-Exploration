using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private GameObject connectionProgressLabel;

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
            connectionProgressLabel.SetActive(connecting);
        }

        public void connect()
        {
            controlUI(true);

            if (PhotonNetwork.IsConnected) //Already connected 
            {
                PhotonNetwork.JoinRandomRoom(); //Try joining a random room
            }
            else //Not connected, connect to Photon server
            {
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to master");

            //This prevents a room from being rejoined when this callback is called after the user leaves a room and returns to the lobby
            if (isConnecting)
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("Disconnected from PUN. Reason {0}", cause);
            controlUI(false);
            isConnecting = false;
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("[JoinRandomFailed] Creating a room. Return Code: " + returnCode + ", Msg: " + message);

            PhotonNetwork.CreateRoom(null,
                new RoomOptions { 
                    MaxPlayers = maxPlayersPerRoom
                }
            );
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined room.");

            //Load next scene in build settings
            PhotonNetwork.LoadLevel(nextSceneIndex);
        }
    }
}
