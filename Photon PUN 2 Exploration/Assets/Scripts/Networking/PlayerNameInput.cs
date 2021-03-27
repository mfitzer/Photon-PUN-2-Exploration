using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace mfitzer.Networking
{
    [RequireComponent(typeof(TMP_InputField))]
    public class PlayerNameInput : MonoBehaviour
    {
        /// <summary>
        /// PlayerPrefs key name used to store the player's name between sessions.
        /// </summary>
        private const string playerNamePrefKey = "PlayerName";

        private TMP_InputField inputField;

        private const string defaultPlayerName = "Player";

        private void Start()
        {
            inputField = GetComponent<TMP_InputField>();
            inputField.onValueChanged.AddListener(setPlayerName);

            loadDefaultName();
        }

        private void loadDefaultName()
        {
            string defaultName = defaultPlayerName;

            //Player has a previously set name, set as default
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                inputField.text = defaultName;
            }

            PhotonNetwork.NickName = defaultName;
        }

        private void setPlayerName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Player name is null or empty.");
                return;
            }

            PhotonNetwork.NickName = value;
            PlayerPrefs.SetString(playerNamePrefKey, value);
        }
    }
}