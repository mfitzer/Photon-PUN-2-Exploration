using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mfitzer.Networking
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        public static GameObject localPlayerInstance { get; private set; }

        private void Awake()
        {
            if (photonView.IsMine)
                localPlayerInstance = gameObject;

            //Prevent the player from being destroyed when switching scenes
            DontDestroyOnLoad(gameObject);
        }
    }
}