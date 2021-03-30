using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace mfDev.XR.Input
{
    [Serializable]
    public class Vector2Event : UnityEvent<Vector2> { }

    /// <summary>
    /// Fires the input event when the input value changes.
    /// </summary>
    public class XRVector2ChangeInputHandler : XRInputHandler
    {
        private Vector2 previousValue = Vector2.zero;

        private Vector2Event OnInputEventOccurred;
        private List<UnityAction<Vector2>> listeners;

        public XRVector2ChangeInputHandler(XRControllerInputUtility inputUtility, XRControllerInputFeature inputFeature) : base(inputUtility, inputFeature)
        {
            OnInputEventOccurred = new Vector2Event();
            listeners = new List<UnityAction<Vector2>>();
        }

        public void addListener(UnityAction<Vector2> listener)
        {
            if (listener != null)
            {
                OnInputEventOccurred.AddListener(listener);
                listeners.Add(listener);

                //Activate input handler now that there is a listener
                activate();
            }
        }

        public void removeListener(UnityAction<Vector2> listener)
        {
            if (listener != null)
            {
                OnInputEventOccurred.RemoveListener(listener);
                listeners.Remove(listener);

                //Deactivate input handler now that there is no listener
                if (listeners.Count == 0)
                    deactivate();
            }
        }

        protected override void updateInput()
        {
            if (inputUtility.tryGetInputFeatureValue(inputFeature, out Vector2 value))
            {
                if (value != previousValue) //value changed
                {
                    previousValue = value;
                    OnInputEventOccurred.Invoke(value);
                }
            }
        }
    }
}