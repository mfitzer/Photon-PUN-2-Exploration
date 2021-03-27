using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace mfitzer.Networking
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (!instance)
                    instance = FindObjectOfType<GameManager>();
                return instance;
            }
        }

        [SerializeField]
        private GameObject playerPrefab;

        private bool applicationQuit = false;

        private void Start()
        {
            //Only instantiate player if local player has not been set
            if (!PlayerManager.localPlayerInstance)
                PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        }

        public override void OnLeftRoom()
        {
            //Prevents bug that causes a scene load to be attempted when the room is left as a result of Application.Quit
            if (!applicationQuit)
            {
                Debug.Log("Returning to launcher");
                SceneManager.LoadScene(0); //Load launcher scene
            }
        }

        public void leaveRoom()
        {
            Debug.Log("Leaving room");
            PhotonNetwork.LeaveRoom();
        }

        private void OnApplicationQuit()
        {
            applicationQuit = true;
        }
    }
}