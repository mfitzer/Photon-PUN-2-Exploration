using System;
using UnityEngine;
using UnityEngine.Events;

namespace mfDev.XR.Input
{
    /// <summary>
    /// Listens for directional input events from a XR2DAxisInput.
    /// </summary>
    [Serializable]
    public class XR2DAxisDirectionalInputListener : XR2DAxisInputListener
    {
        [Tooltip("XR 2D axis input event to listen to.")]
        public XR2DAxisDirectionalInputEvent inputEvent;

        public UnityEvent OnInputEventFired;

        /// <summary>
        /// XRAxisInput corresponding with axisInputFeature.
        /// </summary>
        private XR2DAxisInput xr2DAxisInput;

        /// <summary>
        /// Input handler being listened to.
        /// </summary>
        private XRVector2TargetInputHandler inputHandler;

        public XR2DAxisDirectionalInputListener() { }

        /// <summary>
        /// Initializes the input listener settings based on the given input binding.
        /// </summary>
        public XR2DAxisDirectionalInputListener(XR2DAxisDirectionalInputBinding inputBinding, XRController inputSource)
        {
            axis2DInputFeature = inputBinding.inputFeature;
            inputEvent = inputBinding.inputEvent;
            this.inputSource = inputSource;
            OnInputEventFired = new UnityEvent();
        }

        protected override void startListening()
        {
            inputUtility = XRInputManager.Instance.getXRControllerInputUtility(inputSource);
            XRControllerInputFeature inputFeature = getXRControllerInputFeature(axis2DInputFeature);

            //Found the 2D axis input
            if (inputUtility.tryGetXR2DAxisInput(inputFeature, out xr2DAxisInput))
            {
                inputHandler = getInputHandler(inputEvent, xr2DAxisInput);

                //Subscribe to inputHandler
                inputHandler.addListener(OnInputEventFired.Invoke);
            }
            else //Input unavailable
                deactivate();
        }

        protected override void stopListening()
        {
            if (inputHandler != null)
                inputHandler.removeListener(OnInputEventFired.Invoke);
        }

        /// <summary>
        /// Gets the XRVector2TargetInputHandler associated with the given XRAxisInputEvent.
        /// </summary>
        private XRVector2TargetInputHandler getInputHandler(XR2DAxisDirectionalInputEvent inputEvent, XR2DAxisInput xr2DAxisInput)
        {
            switch (inputEvent)
            {
                case XR2DAxisDirectionalInputEvent.OnAxisLeft:
                    return xr2DAxisInput.OnAxisLeft;
                case XR2DAxisDirectionalInputEvent.OnAxisRight:
                    return xr2DAxisInput.OnAxisRight;
                case XR2DAxisDirectionalInputEvent.OnAxisUp:
                    return xr2DAxisInput.OnAxisUp;
                case XR2DAxisDirectionalInputEvent.OnAxisDown:
                    return xr2DAxisInput.OnAxisDown;
                default:
                    return null;
            }
        }
    }

    /// <summary>
    /// Directional input events available for XR2DAxisInputs.
    /// </summary>
    public enum XR2DAxisDirectionalInputEvent
    {
        OnAxisLeft,
        OnAxisRight,
        OnAxisUp,
        OnAxisDown
    }
}