using mfitzer.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mfitzer.Interactions
{
    public class WristInterface : MonoBehaviour
    {
        public void leaveRoom()
        {
            GameManager.Instance.leaveRoom();
        }
    }
}