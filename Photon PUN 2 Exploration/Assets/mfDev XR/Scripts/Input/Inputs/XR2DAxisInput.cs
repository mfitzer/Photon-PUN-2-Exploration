using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mfDev.XR.Input
{
    public class XR2DAxisInput : XRInput
    {
        public XRVector2ChangeInputHandler OnChange { get; }

        /// <summary>
        /// Called when the axis vector is pointed to the left.
        /// </summary>
        public XRVector2TargetInputHandler OnAxisLeft { get; }

        /// <summary>
        /// Called when the axis vector is pointed to the right.
        /// </summary>
        public XRVector2TargetInputHandler OnAxisRight { get; }

        /// <summary>
        /// Called when the axis vector is pointed to the up.
        /// </summary>
        public XRVector2TargetInputHandler OnAxisUp { get; }

        /// <summary>
        /// Called when the axis vector is pointed to the down.
        /// </summary>
        public XRVector2TargetInputHandler OnAxisDown { get; }

        /// <summary>
        /// Threshold value for direction axis events.
        /// </summary>
        private float directionAxisThreshold = 0.2f;

        public XR2DAxisInput(XRControllerInputUtility inputUtility, XRControllerInputFeature inputFeature) : base(inputUtility, inputFeature)
        {
            OnChange = new XRVector2ChangeInputHandler(inputUtility, inputFeature);

            Vector2 threshold = new Vector2(directionAxisThreshold, directionAxisThreshold);
            OnAxisLeft = new XRVector2TargetInputHandler(inputUtility, inputFeature, Vector2.left, threshold);
            OnAxisRight = new XRVector2TargetInputHandler(inputUtility, inputFeature, Vector2.right, threshold);
            OnAxisUp = new XRVector2TargetInputHandler(inputUtility, inputFeature, Vector2.up, threshold);
            OnAxisDown = new XRVector2TargetInputHandler(inputUtility, inputFeature, Vector2.down, threshold);
        }
    }
}