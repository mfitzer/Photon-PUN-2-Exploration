using mfitzer.Interactions;
using Photon.Pun;
using UnityEngine;

namespace mfitzer.Networking
{
    [RequireComponent(typeof(Grabbable))]
    public class NetworkGrabbable : MonoBehaviourPun
    {
        public Grabbable grabbable { get; private set; }

        private void Start()
        {
            grabbable = GetComponent<Grabbable>();

            grabbable.OnGrab.AddListener(onGrab);
            grabbable.OnRelease.AddListener(onRelease);
        }

        private void onGrab(Hand hand)
        {
            NetworkHand netHand = grabbable.GetComponent<NetworkHand>();
            if (netHand) //Hand is networked
            {
                //Do any extra network grab logic here (mainly handled in the NetworkHand right now)
            }
        }

        private void onRelease(Hand hand)
        {
            NetworkHand netHand = grabbable.GetComponent<NetworkHand>();
            if (netHand) //Hand is networked
            {
                //Do any extra network release logic here (mainly handled in the NetworkHand right now)
            }
        }
    }
}