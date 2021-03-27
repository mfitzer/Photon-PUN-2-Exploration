using Photon.Pun;
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
            SceneManager.LoadScene(0); //Load launcher scene
        }

        public void leaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}