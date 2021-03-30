using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mfDev.XR.Input
{
    [Serializable]
    public abstract class XRInputListener
    {
        [Tooltip("Where the input is coming from.")]
        public XRController inputSource;

        /// <summary>
        /// Input utility to receive input from.
        /// </summary>
        protected XRControllerInputUtility inputUtility;

        /// <summary>
        /// Determines if the listener is active.
        /// </summary>
        public bool active { get; private set; } = false;

        /// <summary>
        /// Starts listening to the specified input event.
        /// </summary>
        public void activate()
        {
            if (!active)
            {
                startListening();
                active = true;
            }
        }

        /// <summary>
        /// Stops listening to the specified input event.
        /// </summary>
        public void deactivate()
        {
            if (active)
            {
                stopListening();
                active = false;
            }
        }

        protected abstract void startListening();

        protected abstract void stopListening();

        ~XRInputListener()
        {
            deactivate();
        }
    }
}