using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;

namespace mfDev.XR.Input
{
    public class XRInputManager : MonoBehaviour
    {
        private static XRInputManager instance;
        public static XRInputManager Instance
        {
            get
            {
                if (!instance)
                    instance = FindObjectOfType<XRInputManager>();
                return instance;
            }
        }

        /// <summary>
        /// Input utility for the input device associated with the left hand.
        /// </summary>
        public XRControllerInputUtility LeftInputUtility
        {
            get
            {
                if (leftInputUtility == null)
                    leftInputUtility = new XRControllerInputUtility(this, InputDevices.GetDeviceAtXRNode(XRNode.LeftHand));
                return leftInputUtility;
            }
        }
        private XRControllerInputUtility leftInputUtility;

        /// <summary>
        /// Input utility for the input device associated with the right hand.
        /// </summary>
        public XRControllerInputUtility RightInputUtility
        {
            get
            {
                if (rightInputUtility == null)
                    rightInputUtility = new XRControllerInputUtility(this, InputDevices.GetDeviceAtXRNode(XRNode.RightHand));
                return rightInputUtility;
            }
        }
        private XRControllerInputUtility rightInputUtility;

        internal delegate void _UpdateInput();

        /// <summary>
        /// Invoked when the input should be updated.
        /// </summary>
        internal event _UpdateInput OnUpdateInput;

        /// <summary>
        /// Gets an XRControllerInputUtility for the given XRController.
        /// </summary>
        public XRControllerInputUtility getXRControllerInputUtility(XRController xrController)
        {
            if (xrController == XRController.LeftController)
                return LeftInputUtility;
            else if (xrController == XRController.RightController)
                return RightInputUtility;
            else
                return null;
        }

        private void Update()
        {
            OnUpdateInput?.Invoke();
        }
    }

    /// <summary>
    /// Indicates a specific XR Controller.
    /// </summary>
    public enum XRController { LeftController, RightController }
}