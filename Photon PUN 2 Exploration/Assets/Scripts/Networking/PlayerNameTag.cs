using Photon.Pun;
using TMPro;
using UnityEngine;

namespace mfitzer.Networking
{
    [RequireComponent(typeof(TextMeshPro))]
    public class PlayerNameTag : MonoBehaviourPun
    {
        private TextMeshPro nameTag;

        private void Start()
        {
            nameTag = GetComponent<TextMeshPro>();
            nameTag.text = photonView.Owner.NickName;
        }
    }
}