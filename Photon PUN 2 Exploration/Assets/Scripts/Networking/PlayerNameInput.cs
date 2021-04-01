using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace mfitzer.Networking
{
    [RequireComponent(typeof(InputField))]
    public class PlayerNameInput : MonoBehaviour
    {
        /// <summary>
        /// PlayerPrefs key name used to store the player's name between sessions.
        /// </summary>
        private const string playerNamePrefKey = "PlayerName";

        private InputField inputField;

        private const string defaultPlayerName = "Player";

        [SerializeField]
        private UnityEvent onValidPlayerNameSet;

        [SerializeField]
        private UnityEvent onInvalidPlayerNameSet;

        private void Start()
        {
            inputField = GetComponent<InputField>();
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

            setPlayerName(defaultName);
        }

        private bool isPlayerNameValid(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                onInvalidPlayerNameSet.Invoke();
                PlayerPrefs.SetString(playerNamePrefKey, "");
                return false;
            }

            onValidPlayerNameSet.Invoke();
            return true;
        }

        private void setPlayerName(string value)
        {
            if (isPlayerNameValid(value))
            {
                PhotonNetwork.NickName = value;
                PlayerPrefs.SetString(playerNamePrefKey, value);
            }
        }
    }
}