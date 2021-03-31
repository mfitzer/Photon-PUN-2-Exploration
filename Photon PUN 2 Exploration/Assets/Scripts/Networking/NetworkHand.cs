using mfitzer.Interactions;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mfitzer.Networking
{
    [RequireComponent(typeof(Hand))]
    public class NetworkHand : MonoBehaviourPun
    {
        public Hand hand { get; private set; }

        private void Start()
        {
            hand = GetComponent<Hand>();

            hand.OnHoverStart.AddListener(onHoverStart);
            hand.OnHoverStop.AddListener(onHoverStop);
            hand.OnGrab.AddListener(onGrab);
            hand.OnRelease.AddListener(onRelease);
        }

        private void onHoverStart(Grabbable grabbable)
        {
            NetworkGrabbable netGrabbable = grabbable.GetComponent<NetworkGrabbable>();
            if (netGrabbable) //Grabbable is networked
            {
                //This is the local hand
                if (photonView.IsMine && PhotonNetwork.IsConnected)
                {
                    //Not the owner and the grabbable is not being grabbed
                    if (!netGrabbable.photonView.AmOwner && !netGrabbable.grabbable.grabbed)
                    {
                        //Take ownership of the grabbable
                        netGrabbable.photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
                    }
                    
                    photonView.RPC("networkHoverStart", RpcTarget.OthersBuffered, netGrabbable.photonView.ViewID);
                }
            }
        }

        [PunRPC]
        public void networkHoverStart(int grabbableViewId)
        {
            PhotonView grabbableView = PhotonView.Find(grabbableViewId);
            if (grabbableView)
            {
                Grabbable grabbable = grabbableView.GetComponent<Grabbable>();
                if (grabbable)
                {
                    Debug.Log("Hover start on network for grabbable: " + grabbable.name);
                }
            }
        }

        private void onHoverStop(Grabbable grabbable)
        {
            NetworkGrabbable netGrabbable = grabbable.GetComponent<NetworkGrabbable>();
            if (netGrabbable) //Grabbable is networked
            {
                //This is the local hand
                if (photonView.IsMine && PhotonNetwork.IsConnected)
                {
                    //This is the local hand
                    if (photonView.IsMine && PhotonNetwork.IsConnected)
                    {
                        photonView.RPC("networkHoverStop", RpcTarget.OthersBuffered, netGrabbable.photonView.ViewID);
                    }
                }
            }
        }

        [PunRPC]
        public void networkHoverStop(int grabbableViewId)
        {
            PhotonView grabbableView = PhotonView.Find(grabbableViewId);
            if (grabbableView)
            {
                Grabbable grabbable = grabbableView.GetComponent<Grabbable>();
                if (grabbable)
                {
                    Debug.Log("Hover stop on network for grabbable: " + grabbable.name);
                }
            }
        }

        private void onGrab(Grabbable grabbable)
        {
            NetworkGrabbable netGrabbable = grabbable.GetComponent<NetworkGrabbable>();
            if (netGrabbable) //Grabbable is networked
            {
                //This is the local hand
                if (photonView.IsMine && PhotonNetwork.IsConnected)
                {
                    //Take ownership of the grabbable
                    netGrabbable.photonView.TransferOwnership(PhotonNetwork.LocalPlayer);

                    photonView.RPC("networkGrab", RpcTarget.OthersBuffered, netGrabbable.photonView.ViewID);
                }
            }
        }

        [PunRPC]
        public void networkGrab(int grabbableViewId)
        {
            Debug.Log("[" + name + "] Network grab");

            PhotonView grabbableView = PhotonView.Find(grabbableViewId);
            if (grabbableView)
            {
                Grabbable grabbable = grabbableView.GetComponent<Grabbable>();
                if (grabbable)
                {
                    hand.grab(grabbable); //Force hand to grab grabbable
                }
            }

        }

        private void onRelease(Grabbable grabbable)
        {
            NetworkGrabbable netGrabbable = grabbable.GetComponent<NetworkGrabbable>();
            if (netGrabbable) //Grabbable is networked
            {
                //This is the local hand
                if (photonView.IsMine && PhotonNetwork.IsConnected)
                {
                    photonView.RPC("networkRelease", RpcTarget.OthersBuffered, netGrabbable.photonView.ViewID);
                }
            }
        }

        [PunRPC]
        public void networkRelease(int grabbableViewId)
        {
            Debug.Log("[" + name + "] Network release");

            PhotonView grabbableView = PhotonView.Find(grabbableViewId);
            if (grabbableView)
            {
                Grabbable grabbable = grabbableView.GetComponent<Grabbable>();
                if (grabbable)
                {
                    hand.release(grabbable); //Force hand to release grabbable
                }
            }

        }
    }
}