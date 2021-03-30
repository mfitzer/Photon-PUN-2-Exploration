using System;
using UnityEngine;

namespace mfDev.XR.Input
{
    [Serializable]
    public class XRAxisInputListener : XRInputListener
    {
        [Tooltip("Which input feature to retrieve axis input from.")]
        public XRControllerAxisInputFeature axisInputFeature;

        [Tooltip("XR axis input event to listen to.")]
        public XRAxisInputEvent inputEvent;

        public FloatEvent OnInputEventFired;

        /// <summary>
        /// XRAxisInput corresponding with axisInputFeature.
        /// </summary>
        private XRAxisInput xrAxisInput;

        /// <summary>
        /// Input handler being listened to.
        /// </summary>
        private XRFloatInputHandler inputHandler;

        public XRAxisInputListener() { }

        /// <summary>
        /// Initializes the input listener settings based on the given input binding.
        /// </summary>
        public XRAxisInputListener(XRAxisInputBinding inputBinding, XRController inputSource)
        {
            axisInputFeature = inputBinding.inputFeature;
            inputEvent = inputBinding.inputEvent;
            this.inputSource = inputSource;
            OnInputEventFired = new FloatEvent();
        }

        protected override void startListening()
        {
            inputUtility = XRInputManager.Instance.getXRControllerInputUtility(inputSource);
            XRControllerInputFeature inputFeature = getXRControllerInputFeature(axisInputFeature);

            //Found the axis input
            if (inputUtility.tryGetXRAxisInput(inputFeature, out xrAxisInput))
            {
                inputHandler = getInputHandler(inputEvent, xrAxisInput);

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
        /// Gets the XRFloatInputHandler associated with the given XRAxisInputEvent.
        /// </summary>
        private XRFloatInputHandler getInputHandler(XRAxisInputEvent inputEvent, XRAxisInput xrAxisInput)
        {
            switch (inputEvent)
            {
                case XRAxisInputEvent.OnChange:
                    return xrAxisInput.OnChange;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the XRControllerInputFeature corresponding to the XRControllerAxisInputFeature.
        /// </summary>
        public static XRControllerInputFeature getXRControllerInputFeature(XRControllerAxisInputFeature axisInputFeature)
        {
            if (Enum.TryParse(axisInputFeature.ToString(), out XRControllerInputFeature inputFeature))
                return inputFeature;
            else return default;
        }
    }

    /// <summary>
    /// All XRControllerInputFeatures correlating with an axis.
    /// </summary>
    public enum XRControllerAxisInputFeature
    {
        Trigger,
        Grip,
        BatteryLevel
    }

    /// <summary>
    /// Input events available for XRAxisInputs.
    /// </summary>
    public enum XRAxisInputEvent
    {
        OnChange
    }
}