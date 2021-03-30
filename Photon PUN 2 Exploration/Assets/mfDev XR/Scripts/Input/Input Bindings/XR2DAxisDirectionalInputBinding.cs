using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mfDev.XR.Input
{
    [System.Serializable]
    public class XR2DAxisDirectionalInputBinding : XRInputBinding
    {
        [Tooltip("Which input feature to retrieve 2D axis input from.")]
        public XRController2DAxisInputFeature inputFeature;

        [Tooltip("XR 2D axis input event to listen to.")]
        public XR2DAxisDirectionalInputEvent inputEvent;
    }
}