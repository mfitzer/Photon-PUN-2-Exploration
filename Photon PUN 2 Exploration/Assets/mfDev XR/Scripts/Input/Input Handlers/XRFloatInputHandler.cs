using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace mfDev.XR.Input
{
    [System.Serializable]
    public class FloatEvent : UnityEvent<float> { }

    public class XRFloatInputHandler : XRInputHandler
    {
        private float previousValue = 0f;

        private FloatEvent OnValueChanged;
        private List<UnityAction<float>> listeners;

        public XRFloatInputHandler(XRControllerInputUtility inputUtility, XRControllerInputFeature inputFeature) : base(inputUtility, inputFeature)
        {
            OnValueChanged = new FloatEvent();
            listeners = new List<UnityAction<float>>();
        }

        public void addListener(UnityAction<float> listener)
        {
            if (listener != null)
            {
                OnValueChanged.AddListener(listener);
                listeners.Add(listener);

                //Activate input handler now that there is a listener
                activate();
            }
        }

        public void removeListener(UnityAction<float> listener)
        {
            if (listener != null)
            {
                OnValueChanged.RemoveListener(listener);
                listeners.Remove(listener);

                //Deactivate input handler now that there is no listener
                if (listeners.Count == 0)
                    deactivate();
            }
        }

        protected override void updateInput()
        {
            if (inputUtility.tryGetInputFeatureValue(inputFeature, out float value))
            {
                //value changed
                if (value != previousValue)
                {
                    previousValue = value;
                    OnValueChanged.Invoke(value);
                }
            }
        }
    }
}