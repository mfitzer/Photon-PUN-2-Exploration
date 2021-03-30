using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mfDev.XR.Input
{
    [System.Serializable]
    public class XRAxisInputBinding : XRInputBinding
    {
        [Tooltip("Which input feature to retrieve axis input from.")]
        public XRControllerAxisInputFeature inputFeature;

        [Tooltip("XR axis input event to listen to.")]
        public XRAxisInputEvent inputEvent;
    }
}