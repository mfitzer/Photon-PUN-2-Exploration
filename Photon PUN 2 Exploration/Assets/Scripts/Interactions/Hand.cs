using UnityEngine;
using UnityEngine.Events;

namespace mfitzer.Interactions
{
    [System.Serializable]
    public class GrabbableEvent : UnityEvent<Grabbable> { }

    public class Hand : MonoBehaviour
    {
        [SerializeField, ReadOnlyField]
        private Grabbable hoveringGrabbable;

        [SerializeField, ReadOnlyField]
        private Grabbable activeGrabbable;

        #region Events

        [SerializeField]
        private GrabbableEvent onHoverStart;
        public GrabbableEvent OnHoverStart
        {
            get => onHoverStart;
        }
        [SerializeField]
        private GrabbableEvent onHoverStop;
        public GrabbableEvent OnHoverStop
        {
            get => onHoverStop;
        }
        [SerializeField]
        private GrabbableEvent onGrab;
        public GrabbableEvent OnGrab
        {
            get => onGrab;
        }
        [SerializeField]
        private GrabbableEvent onRelease;
        public GrabbableEvent OnRelease
        {
            get => onRelease;
        }

        #endregion Events

        private bool grabbing { get => activeGrabbable; }

        private void OnTriggerEnter(Collider other)
        {
            Grabbable grabbable = other.GetComponent<Grabbable>();
            if (grabbable)
            {
                hoveringGrabbable = grabbable;
                OnHoverStart.Invoke(hoveringGrabbable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Grabbable grabbable = other.GetComponent<Grabbable>();
            if (grabbable && grabbable == hoveringGrabbable)
            {
                OnHoverStop.Invoke(hoveringGrabbable);
                hoveringGrabbable = null;
            }
        }

        /// <summary>
        /// Handles the grab input action being taken.
        /// </summary>
        public void handleGrab()
        {
            if (!grabbing && hoveringGrabbable)
                grab(hoveringGrabbable);
        }

        public void grab(Grabbable grabbable)
        {
            if (grabbable && grabbable != activeGrabbable)
            {
                Debug.Log("Grab " + grabbable.name);

                if (activeGrabbable)
                    activeGrabbable.processRelease(this);

                activeGrabbable = grabbable;
                hoveringGrabbable = null;
                activeGrabbable.processGrab(this);

                onGrab.Invoke(activeGrabbable);
            }
        }

        /// <summary>
        /// Handles the grab input action being taken.
        /// </summary>
        public void handleRelease()
        {
            if (grabbing)
                release(activeGrabbable);
        }

        public void release(Grabbable grabbable)
        {
            if (grabbable && grabbable == activeGrabbable)
            {
                Debug.Log("Release " + grabbable.name);

                grabbable.processRelease(this);
                onRelease.Invoke(grabbable);
                activeGrabbable = null;
            }
        }
    }
}