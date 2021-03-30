using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mfDev.XR.Input
{
    public class XRAxisInput : XRInput
    {
        public XRFloatInputHandler OnChange { get; }

        public XRAxisInput(XRControllerInputUtility inputUtility, XRControllerInputFeature inputFeature) : base(inputUtility, inputFeature)
        {
            OnChange = new XRFloatInputHandler(inputUtility, inputFeature);
        }
    }
}