using System;
using UnityEngine;
using UnityEngine.Events;

namespace mfDev.XR.Input
{
    [Serializable]
    public class XRButtonInputListener : XRInputListener
    {
        [Tooltip("Which input feature to retrieve button input from.")]
        public XRControllerButtonInputFeature buttonInputFeature;

        [Tooltip("XR button input event to listen to.")]
        public XRButtonInputEvent inputEvent;

        public UnityEvent OnInputEventFired;

        /// <summary>
        /// XRButtonInput corresponding with buttonInputFeature.
        /// </summary>
        private XRButtonInput xrButtonInput;

        /// <summary>
        /// Input handler being listened to.
        /// </summary>
        private XRBoolInputHandler inputHandler;

        public XRButtonInputListener() { }

        /// <summary>
        /// Initializes the input listener settings based on the given input binding.
        /// </summary>
        public XRButtonInputListener(XRButtonInputBinding inputBinding, XRController inputSource)
        {
            buttonInputFeature = inputBinding.inputFeature;
            inputEvent = inputBinding.inputEvent;
            this.inputSource = inputSource;
            OnInputEventFired = new UnityEvent();
        }

        protected override void startListening()
        {
            inputUtility = XRInputManager.Instance.getXRControllerInputUtility(inputSource);
            XRControllerInputFeature inputFeature = getXRControllerInputFeature(buttonInputFeature);

            //Found the button input
            if (inputUtility.tryGetXRButtonInput(inputFeature, out xrButtonInput))
            {
                inputHandler = getInputHandler(inputEvent, xrButtonInput);

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
        /// Gets the XRBoolInputHandler associated with the given XRButtonInputEvent.
        /// </summary>
        private XRBoolInputHandler getInputHandler(XRButtonInputEvent inputEvent, XRButtonInput xrButtonInput)
        {
            switch (inputEvent)
            {
                case XRButtonInputEvent.OnButtonPressed:
                    return xrButtonInput.OnButtonPressed;
                case XRButtonInputEvent.OnButtonReleased:
                    return xrButtonInput.OnButtonReleased;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the XRControllerInputFeature corresponding to the XRControllerButtonInputFeature.
        /// </summary>
        public static XRControllerInputFeature getXRControllerInputFeature(XRControllerButtonInputFeature buttonInputFeature)
        {
            if (Enum.TryParse(buttonInputFeature.ToString(), out XRControllerInputFeature inputFeature))
                return inputFeature;
            else return default;
        }
    }

    /// <summary>
    /// All XRControllerInputFeatures correlating with a button.
    /// </summary>
    public enum XRControllerButtonInputFeature
    {
        Secondary2DAxisClick,
        PrimaryButton,
        PrimaryTouch,
        SecondaryButton,
        SecondaryTouch,
        GripButton,
        TriggerButton,
        MenuButton,
        Primary2DAxisClick,
        Primary2DAxisTouch,
        UserPresence
    }

    /// <summary>
    /// Input events available for XRButtonInputs.
    /// </summary>
    public enum XRButtonInputEvent
    {
        OnButtonPressed,
        OnButtonReleased
    }
}