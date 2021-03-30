using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace mfDev.XR.Input
{
    public class XRButtonInput : XRInput
    {
        public XRBoolInputHandler OnButtonPressed { get; }
        public XRBoolInputHandler OnButtonReleased { get; }

        public XRButtonInput(XRControllerInputUtility inputUtility, XRControllerInputFeature inputFeature) : base(inputUtility, inputFeature)
        {
            OnButtonPressed = new XRBoolInputHandler(inputUtility, inputFeature, true);
            OnButtonReleased = new XRBoolInputHandler(inputUtility, inputFeature, false);
        }
    }
}