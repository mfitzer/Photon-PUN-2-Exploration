using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mfDev.XR.Input
{
    [System.Serializable]
    public class XRButtonInputBinding : XRInputBinding
    {
        [Tooltip("Which input feature to retrieve button input from.")]
        public XRControllerButtonInputFeature inputFeature;

        [Tooltip("XR button input event to listen to.")]
        public XRButtonInputEvent inputEvent;
    }
}