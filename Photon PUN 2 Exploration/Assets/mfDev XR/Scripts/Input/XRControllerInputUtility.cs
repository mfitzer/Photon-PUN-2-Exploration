using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace mfDev.XR.Input
{
    public class XRControllerInputUtility
    {
        public XRInputManager inputManager { get; }

        /// <summary>
        /// Determines if input will be updated for this input device.
        /// </summary>        
        public bool active { get; private set; } = false;

        /// <summary>
        /// Device to recieve input from.
        /// </summary>
        public InputDevice InputDevice
        {
            get
            {
                return inputDevice;
            }
            internal set
            {
                //value is not default and is a different InputDevice
                if (value != default && value != inputDevice)
                {
                    inputDevice = value;
                    initialize();
                }
            }
        }
        private InputDevice inputDevice;

        #region Available Input Features

        /// <summary>
        /// All available input features on this device.
        /// </summary>
        private Dictionary<string, InputFeatureUsage> inputFeatures;

        /// <summary>
        /// Bool valued input features available to the inputDevice.
        /// </summary>
        private Dictionary<XRControllerInputFeature, InputFeatureUsage<bool>> buttonInputFeatures;

        /// <summary>
        /// Float valued input features available to the inputDevice.
        /// </summary>
        private Dictionary<XRControllerInputFeature, InputFeatureUsage<float>> axisInputFeatures;

        /// <summary>
        /// Vector2 valued input features available to the inputDevice.
        /// </summary>
        private Dictionary<XRControllerInputFeature, InputFeatureUsage<Vector2>> axis2DInputFeatures;

        #endregion Input Features

        private Dictionary<XRControllerInputFeature, XRButtonInput> xrButtonInputs;
        private Dictionary<XRControllerInputFeature, XRAxisInput> xrAxisInputs;
        private Dictionary<XRControllerInputFeature, XR2DAxisInput> xr2DAxisInputs;
        
        /// <summary>
        /// Invoked when the input should be updated.
        /// </summary>
        internal event XRInputManager._UpdateInput OnUpdateInput;

        public XRControllerInputUtility(XRInputManager inputManager, InputDevice inputDevice)
        {
            this.inputManager = inputManager;
            InputDevice = inputDevice;
        }

        //Destructor
        ~XRControllerInputUtility()
        {
            deactivate();
        }

        /// <summary>
        /// Performs necessary initializations for the input device.
        /// </summary>
        private void initialize()
        {
            Debug.Log("[mfDev.XR] Initializing XRControllerInputUtility");

            getAvailableInputFeatures();
            mapInputFeatures();
            mapXRInputs();
            activate();
        }

        #region Input Features

        /// <summary>
        /// Gets all available input features for the given input device.
        /// </summary>
        private void getAvailableInputFeatures()
        {
            Debug.Log("[mfDev.XR] Getting available input features for controller: " + InputDevice.name);

            inputFeatures = new Dictionary<string, InputFeatureUsage>();
            List<InputFeatureUsage> availableInputFeatures = new List<InputFeatureUsage>();
            if (InputDevice.TryGetFeatureUsages(availableInputFeatures))
            {
                foreach (InputFeatureUsage inputFeature in availableInputFeatures)
                {
                    inputFeatures.Add(inputFeature.name, inputFeature);
                    
                    Debug.Log("[mfDev.XR] " + inputFeature.name + " input feature is available on " + InputDevice.name);
                }
            }
            else
            {
                Debug.Log("[mfDev.XR] No input feature usages found on " + InputDevice.name);
            }
        }

        #region Input Feature Mapping

        /// <summary>
        /// Maps all XRInputFeatures to a InputFeatureUsage object in the appropriate Dictionary.
        /// </summary>
        private void mapInputFeatures()
        {
            Debug.Log("[mfDev.XR] Mapping input features for controller: " + InputDevice.name);

            //Map button input features
            buttonInputFeatures = new Dictionary<XRControllerInputFeature, InputFeatureUsage<bool>>();
            addButtonInputFeature(XRControllerInputFeature.Secondary2DAxisClick, CommonUsages.secondary2DAxisClick);
            addButtonInputFeature(XRControllerInputFeature.PrimaryButton, CommonUsages.primaryButton);
            addButtonInputFeature(XRControllerInputFeature.PrimaryTouch, CommonUsages.primaryTouch);
            addButtonInputFeature(XRControllerInputFeature.SecondaryButton, CommonUsages.secondaryButton);
            addButtonInputFeature(XRControllerInputFeature.SecondaryTouch, CommonUsages.secondaryTouch);
            addButtonInputFeature(XRControllerInputFeature.GripButton, CommonUsages.gripButton);
            addButtonInputFeature(XRControllerInputFeature.TriggerButton, CommonUsages.triggerButton);
            addButtonInputFeature(XRControllerInputFeature.MenuButton, CommonUsages.menuButton);
            addButtonInputFeature(XRControllerInputFeature.Primary2DAxisClick, CommonUsages.primary2DAxisClick);
            addButtonInputFeature(XRControllerInputFeature.Primary2DAxisTouch, CommonUsages.primary2DAxisTouch);
            addButtonInputFeature(XRControllerInputFeature.UserPresence, CommonUsages.userPresence);

            //Map axis input features
            axisInputFeatures = new Dictionary<XRControllerInputFeature, InputFeatureUsage<float>>();
            addAxisInputFeature(XRControllerInputFeature.Trigger, CommonUsages.trigger);
            addAxisInputFeature(XRControllerInputFeature.Grip, CommonUsages.grip);
            addAxisInputFeature(XRControllerInputFeature.BatteryLevel, CommonUsages.batteryLevel);

            //Map 2D axis input features
            axis2DInputFeatures = new Dictionary<XRControllerInputFeature, InputFeatureUsage<Vector2>>();
            add2DAxisInputFeature(XRControllerInputFeature.Primary2DAxis, CommonUsages.primary2DAxis);
            add2DAxisInputFeature(XRControllerInputFeature.Secondary2DAxis, CommonUsages.secondary2DAxis);
        }

        /// <summary>
        /// Adds a new InputFeatureUsage<bool> to the button input features dictionary for the given XRInputFeature
        /// if it is available on the InputDevice.
        /// </summary>
        private void addButtonInputFeature(XRControllerInputFeature inputFeatureName, InputFeatureUsage<bool> buttonInputFeature)
        {
            //Input feature is available
            if (inputFeatures.ContainsKey(inputFeatureName.ToString()))
            {
                Debug.Log("[mfDev.XR] Adding button feature: " + inputFeatureName);
                buttonInputFeatures.Add(inputFeatureName, buttonInputFeature);
            }
            else
                Debug.Log("[mfDev.XR] Button feature unavailable, cannot add: " + inputFeatureName);
        }

        /// <summary>
        /// Adds a new InputFeatureUsage<float> to the button input features dictionary for the given XRInputFeature
        /// if it is available on the InputDevice.
        /// </summary>
        private void addAxisInputFeature(XRControllerInputFeature inputFeatureName, InputFeatureUsage<float> axisInputFeature)
        {
            //Input feature is available
            if (inputFeatures.ContainsKey(inputFeatureName.ToString()))
            {
                Debug.Log("[mfDev.XR] Adding axis feature: " + inputFeatureName);
                axisInputFeatures.Add(inputFeatureName, axisInputFeature);
            }
            else
                Debug.Log("[mfDev.XR] Axis feature unavailable, cannot add: " + inputFeatureName);
        }

        /// <summary>
        /// Adds a new InputFeatureUsage<Vector2> to the button input features dictionary for the given XRInputFeature
        /// if it is available on the InputDevice.
        /// </summary>
        private void add2DAxisInputFeature(XRControllerInputFeature inputFeatureName, InputFeatureUsage<Vector2> axis2DInputFeature)
        {
            //Input feature is available
            if (inputFeatures.ContainsKey(inputFeatureName.ToString()))
            {
                Debug.Log("[mfDev.XR] Adding 2D axis feature: " + inputFeatureName);
                axis2DInputFeatures.Add(inputFeatureName, axis2DInputFeature);
            }
            else
                Debug.Log("[mfDev.XR] 2D Axis feature unavailable, cannot add: " + inputFeatureName);
        }

        #endregion Input Feature Mapping

        #region Input Feature Values

        /// <summary>
        /// Attempts to get the InputFeatureUsage matching the provided XRInputFeature.
        /// </summary>
        /// <param name="inputFeatureName">Input feature to retrieve.</param>
        /// <param name="value">Value of the InputFeatureUsage.</param>
        /// <returns>Returns bool indicating if the input feature was found.</returns>
        public bool tryGetInputFeatureValue(XRControllerInputFeature inputFeatureName, out bool value)
        {
            if (active)
            {
                //Input feature value available
                if (InputDevice.TryGetFeatureValue(buttonInputFeatures[inputFeatureName], out value))
                    return true;
            }
            else
                value = false;

            //Input feature value unavailable
            return false;
        }

        /// <summary>
        /// Attempts to get the InputFeatureUsage matching the provided XRInputFeature.
        /// </summary>
        /// <param name="inputFeatureName">Input feature to retrieve.</param>
        /// <param name="value">Value of the InputFeatureUsage.</param>
        /// <returns>Returns bool indicating if the input feature was found.</returns>
        public bool tryGetInputFeatureValue(XRControllerInputFeature inputFeatureName, out float value)
        {
            if (active)
            {
                //Input feature value available
                if (InputDevice.TryGetFeatureValue(axisInputFeatures[inputFeatureName], out value))
                    return true;
            }
            else
                value = -1f;

            //Input feature value unavailable
            return false;
        }

        /// <summary>
        /// Attempts to get the InputFeatureUsage matching the provided XRInputFeature.
        /// </summary>
        /// <param name="inputFeatureName">Input feature to retrieve.</param>
        /// <param name="value">Value of the InputFeatureUsage.</param>
        /// <returns>Returns bool indicating if the input feature was found.</returns>
        public bool tryGetInputFeatureValue(XRControllerInputFeature inputFeatureName, out Vector2 value)
        {
            if (active)
            {
                //Input feature value available
                if (InputDevice.TryGetFeatureValue(axis2DInputFeatures[inputFeatureName], out value))
                    return true;
            }
            else
                value = Vector2.zero;

            //Input feature value unavailable
            return false;
        }

        #endregion Input Feature Values

        #endregion Input Features

        #region XR Inputs

        /// <summary>
        /// Maps available XRInputFeatures to XRInputs.
        /// </summary>
        private void mapXRInputs()
        {
            Debug.Log("[mfDev.XR] Mapping XRInputs for controller: " + InputDevice.name);

            xrButtonInputs = new Dictionary<XRControllerInputFeature, XRButtonInput>();
            foreach (XRControllerInputFeature inputFeature in buttonInputFeatures.Keys)
            {
                Debug.Log("[mfDev.XR] Adding XRButtonInput for: " + inputFeature);
                xrButtonInputs.Add(inputFeature, new XRButtonInput(this, inputFeature));
            }

            xrAxisInputs = new Dictionary<XRControllerInputFeature, XRAxisInput>();
            foreach (XRControllerInputFeature inputFeature in axisInputFeatures.Keys)
            {
                Debug.Log("[mfDev.XR] Adding XRAxisInput for: " + inputFeature);
                xrAxisInputs.Add(inputFeature, new XRAxisInput(this, inputFeature));
            }

            xr2DAxisInputs = new Dictionary<XRControllerInputFeature, XR2DAxisInput>();
            foreach (XRControllerInputFeature inputFeature in axis2DInputFeatures.Keys)
            {
                Debug.Log("[mfDev.XR] Adding XR2DAxisInput for: " + inputFeature);
                xr2DAxisInputs.Add(inputFeature, new XR2DAxisInput(this, inputFeature));
            }
        }

        /// <summary>
        /// Gets a XRButtonInput for a given XRInputFeature.
        /// </summary>
        /// <returns>Bool indicating if the XRInput was found.</returns>
        public bool tryGetXRButtonInput(XRControllerInputFeature inputFeature, out XRButtonInput buttonInput)
        {
            if (active)
            {
                if (xrButtonInputs.TryGetValue(inputFeature, out buttonInput))
                    return true;
                else
                    Debug.LogError("[mfDev.XR] XRButtonInput unavailable for input feature: " + inputFeature);
            }
            else
                buttonInput = null;

            return false;
        }

        /// <summary>
        /// Gets a XRAxisInput for a given XRInputFeature.
        /// </summary>
        /// <returns>Bool indicating if the XRInput was found.</returns>
        public bool tryGetXRAxisInput(XRControllerInputFeature inputFeature, out XRAxisInput axisInput)
        {
            if (active)
            {
                if (xrAxisInputs.TryGetValue(inputFeature, out axisInput))
                    return true;
                else
                    Debug.LogError("[mfDev.XR] XRAxisInput unavailable for input feature: " + inputFeature);
            }
            else
                axisInput = null;

            return false;
        }

        /// <summary>
        /// Gets a XR2DAxisInput for a given XRInputFeature.
        /// </summary>
        /// <returns>Bool indicating if the XRInput was found.</returns>
        public bool tryGetXR2DAxisInput(XRControllerInputFeature inputFeature, out XR2DAxisInput axis2DInput)
        {
            if (active)
            {
                if (xr2DAxisInputs.TryGetValue(inputFeature, out axis2DInput))
                    return true;
                else
                    Debug.LogError("[mfDev.XR] XR2DAxisInput unavailable for input feature: " + inputFeature);
            }
            else
                axis2DInput = null;

            return false;
        }

        #endregion XR Inputs

        private void updateInput()
        {
            OnUpdateInput?.Invoke();
        }

        /// <summary>
        /// Enables input tracking for this input device.
        /// </summary>
        public void activate()
        {
            if (!active)
            {
                Debug.Log("[mfDev.XR] Activating input utility for: " + InputDevice.name);
                inputManager.OnUpdateInput += updateInput;
                active = true;
            }
        }

        /// <summary>
        /// Disables input tracking for this input device.
        /// </summary>
        public void deactivate()
        {
            if (active)
            {
                Debug.Log("[mfDev.XR] Deactivating input utility for: " + InputDevice.name);
                inputManager.OnUpdateInput -= updateInput;
                active = false;
            }
        }
    }

    /// <summary>
    /// Representation of an InputFeatureUsage for an XR controller by name.
    /// </summary>
    public enum XRControllerInputFeature
    {
        Primary2DAxis,
        Trigger,
        Grip,
        Secondary2DAxis,
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
        BatteryLevel,
        UserPresence
    }
}