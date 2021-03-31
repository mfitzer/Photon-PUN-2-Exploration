using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace mfitzer.Interactions
{
    [System.Serializable]
    public class HandEvent : UnityEvent<Hand> { }

    [RequireComponent(typeof(Rigidbody))]
    public class Grabbable : MonoBehaviour
    {
        private Rigidbody rb;
        private Hand grabbingHand;
        private Transform initParent;

        [SerializeField]
        private HandEvent onGrab;
        public HandEvent OnGrab
        {
            get => onGrab;
        }
        [SerializeField]
        private HandEvent onRelease;
        public HandEvent OnRelease
        {
            get => onRelease;
        }

        /// <summary>
        /// Indicates if the grabbable is currently being grabbed.
        /// </summary>
        public bool grabbed
        {
            get
            {
                return grabbingHand;
            }
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            initParent = transform.parent;
        }

        public void processGrab(Hand hand)
        {
            Debug.Log("[" + name + "] Processing grab from hand: " + hand.name);

            if (grabbingHand)
                grabbingHand.handleRelease();

            grabbingHand = hand;
            rb.isKinematic = true;
            transform.parent = grabbingHand.transform;

            OnGrab.Invoke(hand);
        }

        public void processRelease(Hand hand)
        {
            Debug.Log("[" + name + "] Processing release from hand: " + hand.name);

            if (grabbingHand)
            {
                grabbingHand = null;
                rb.isKinematic = false;
                transform.parent = initParent;

                OnRelease.Invoke(hand);
            }
        }
    }
}