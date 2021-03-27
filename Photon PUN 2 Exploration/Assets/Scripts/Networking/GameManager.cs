using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace mfitzer.Networking
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public override void OnLeftRoom()
        {
            Debug.Log("Returning to launcher");
            SceneManager.LoadScene(0); //Load launcher scene
        }

        public void leaveRoom()
        {
            Debug.Log("Leaving room");
            PhotonNetwork.LeaveRoom();
        }
    }
}