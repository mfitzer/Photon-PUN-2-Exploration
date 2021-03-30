using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace mfDev.XR.Input
{
    public abstract class XRInputHandler
    {
        protected XRControllerInputUtility inputUtility;
        public XRControllerInputFeature inputFeature { get; }

        /// <summary>
        /// Determines if the input handler is actively checking input.
        /// </summary>
        public bool active { get; private set; } = false;

        public XRInputHandler(XRControllerInputUtility inputUtility, XRControllerInputFeature inputFeature)
        {
            this.inputUtility = inputUtility;
            this.inputFeature = inputFeature;
        }

        /// <summary>
        /// Activates the input handler to start processing input events.
        /// </summary>
        protected void activate()
        {
            if (!active)
            {
                Debug.Log("[mfDev.XR] Activating input handling for: " + inputFeature);
                inputUtility.OnUpdateInput += updateInput;
                active = true;
            }
        }

        /// <summary>
        /// Deactivates the input handler to stop processing input events.
        /// </summary>
        protected void deactivate()
        {
            if (active)
            {
                Debug.Log("[mfDev.XR] Deactivating input handling for: " + inputFeature);
                inputUtility.OnUpdateInput -= updateInput;
                active = false;
            }
        }

        protected abstract void updateInput();

        //Destructor
        ~XRInputHandler()
        {
            deactivate();
        }
    }
}