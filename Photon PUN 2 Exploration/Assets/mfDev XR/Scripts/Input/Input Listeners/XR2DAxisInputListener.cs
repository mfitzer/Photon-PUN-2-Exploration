using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mfDev.XR.Input
{
    [Serializable]
    public abstract class XR2DAxisInputListener : XRInputListener
    {
        [Tooltip("Which input feature to retrieve 2D axis input from.")]
        public XRController2DAxisInputFeature axis2DInputFeature;

        /// <summary>
        /// Gets the XRControllerInputFeature corresponding to the XRController2DAxisInputFeature.
        /// </summary>
        public static XRControllerInputFeature getXRControllerInputFeature(XRController2DAxisInputFeature axis2DInputFeature)
        {
            if (Enum.TryParse(axis2DInputFeature.ToString(), out XRControllerInputFeature inputFeature))
                return inputFeature;
            else return default;
        }
    }

    /// <summary>
    /// All XRControllerInputFeatures correlating with a 2D axis.
    /// </summary>
    public enum XRController2DAxisInputFeature
    {
        Primary2DAxis,
        Secondary2DAxis
    }
}