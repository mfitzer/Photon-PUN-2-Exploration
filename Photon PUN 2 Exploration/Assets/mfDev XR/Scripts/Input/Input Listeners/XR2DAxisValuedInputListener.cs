using System;
using UnityEngine;

namespace mfDev.XR.Input
{
    /// <summary>
    /// Listens for Vector2 valued input events from a XR2DAxisInput.
    /// </summary>
    [Serializable]
    public class XR2DAxisValuedInputListener : XR2DAxisInputListener
    {
        [Tooltip("XR 2D axis input event to listen to.")]
        public XR2DAxisValuedInputEvent inputEvent;

        public Vector2Event OnInputEventFired;

        /// <summary>
        /// XR2DAxisInput corresponding with axis2DInputFeature.
        /// </summary>
        private XR2DAxisInput xr2DAxisInput;

        /// <summary>
        /// Input handler being listened to.
        /// </summary>
        private XRVector2ChangeInputHandler inputHandler;

        public XR2DAxisValuedInputListener() { }

        /// <summary>
        /// Initializes the input listener settings based on the given input binding.
        /// </summary>
        public XR2DAxisValuedInputListener(XR2DAxisValuedInputBinding inputBinding, XRController inputSource)
        {
            axis2DInputFeature = inputBinding.inputFeature;
            inputEvent = inputBinding.inputEvent;
            this.inputSource = inputSource;
            OnInputEventFired = new Vector2Event();
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
        /// Gets the XRVector2ChangeInputHandler associated with the given XR2DAxisInputEvent.
        /// </summary>
        private XRVector2ChangeInputHandler getInputHandler(XR2DAxisValuedInputEvent inputEvent, XR2DAxisInput xr2DAxisInput)
        {
            switch (inputEvent)
            {
                case XR2DAxisValuedInputEvent.OnChange:
                    return xr2DAxisInput.OnChange;
                default:
                    return null;
            }
        }
    }

    /// <summary>
    /// Vector2 valued input events available for XR2DAxisInputs.
    /// </summary>
    public enum XR2DAxisValuedInputEvent
    {
        OnChange
    }
}