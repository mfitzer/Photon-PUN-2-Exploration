using Photon.Pun;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace mfitzer.Interactions
{
    public class NetworkXRRig : MonoBehaviourPun
    {
        [SerializeField]
        private XRRig xrRig;

        private TeleportationArea[] teleportAreas;
        private TeleportationProvider teleportProvider;

        private void Start()
        {
            teleportAreas = FindObjectsOfType<TeleportationArea>();

            configure();
        }

        private void configure()
        {
            //Locally controlled, enable XR rig for player movement inputs
            if (photonView.IsMine && PhotonNetwork.IsConnected)
            {
                xrRig.gameObject.SetActive(true);

                /*Teleportation provider is set on Awake in TeleportationAreas,
                 * but TeleportationProvider is not present in the scen at that time, need to manually set it */
                if (!teleportProvider)
                {
                    teleportProvider = GetComponentInChildren<TeleportationProvider>();
                    foreach (TeleportationArea teleportArea in teleportAreas)
                        teleportArea.teleportationProvider = teleportProvider;
                }
            }
            
        }
    }
}
