using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mfDev.XR.Input
{
    public abstract class XRInput
    {
        protected XRControllerInputUtility inputUtility;
        public XRControllerInputFeature inputFeature;

        public XRInput(XRControllerInputUtility inputUtility, XRControllerInputFeature inputFeature)
        {
            this.inputUtility = inputUtility;
            this.inputFeature = inputFeature;
        }
    }
}