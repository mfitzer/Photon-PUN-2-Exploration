using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace mfDev.XR.Input
{
    public class XRBoolInputHandler : XRInputHandler
    {
        private bool targetValue;
        private bool previousValue = false;

        private UnityEvent OnTargetValueReached;
        private List<UnityAction> listeners;

        public XRBoolInputHandler(XRControllerInputUtility inputUtility, XRControllerInputFeature inputFeature, bool targetValue) : base(inputUtility, inputFeature)
        {
            this.targetValue = targetValue;
            OnTargetValueReached = new UnityEvent();
            listeners = new List<UnityAction>();
        }

        public void addListener(UnityAction listener)
        {
            if (listener != null)
            {
                OnTargetValueReached.AddListener(listener);
                listeners.Add(listener);

                //Activate input handler now that there is a listener
                activate();
            }
        }

        public void removeListener(UnityAction listener)
        {
            if (listener != null)
            {
                OnTargetValueReached.RemoveListener(listener);
                listeners.Remove(listener);

                //Deactivate input handler now that there is no listener
                if (listeners.Count == 0)
                    deactivate();
            }
        }

        protected override void updateInput()
        {
            if (inputUtility.tryGetInputFeatureValue(inputFeature, out bool value))
            {
                if (value != previousValue)
                {
                    //Just reached the target value
                    if (value == targetValue && previousValue == !targetValue)
                    {
                        OnTargetValueReached.Invoke();
                    }

                    previousValue = value;
                }
            }
        }
    }
}