using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace mfDev.XR.Input
{
    /// <summary>
    /// Fires the input event when the input value reaches a given target value within a given threshold.
    /// </summary>
    public class XRVector2TargetInputHandler : XRInputHandler
    {
        /// <summary>
        /// Target value the input must reach to fire the input event.
        /// </summary>
        private Vector2 targetValue;

        /// <summary>
        /// How close to the targetValue the input must be before the event will be fired.
        /// </summary>
        private Vector2 threshold;

        /// <summary>
        /// Indicates if the input is currently within the threshold.
        /// </summary>
        private bool withinThreshold = false;

        private UnityEvent OnInputEventOccurred;
        private List<UnityAction> listeners;
        
        public XRVector2TargetInputHandler(XRControllerInputUtility inputUtility, XRControllerInputFeature inputFeature, Vector2 targetValue, Vector2 threshold)
            : base(inputUtility, inputFeature)
        {
            this.targetValue = targetValue;
            this.threshold = threshold;
            OnInputEventOccurred = new UnityEvent();
            listeners = new List<UnityAction>();
        }

        public void addListener(UnityAction listener)
        {
            if (listener != null)
            {
                OnInputEventOccurred.AddListener(listener);
                listeners.Add(listener);

                //Activate input handler now that there is a listener
                activate();
            }
        }

        public void removeListener(UnityAction listener)
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
                Vector2 minValue = targetValue - threshold;
                Vector2 maxValue = targetValue + threshold;

                bool aboveMin = minValue.x <= value.x && minValue.y <= value.y;
                bool belowMax = maxValue.x >= value.x && maxValue.y >= value.y;

                //Debug.Log("[mfDev.XR] " + inputFeature + " aboveMin: " + aboveMin + " | belowMax: " + belowMax + " | value: " + value);

                //Within threshold
                if (aboveMin && belowMax)
                {
                    if (!withinThreshold) //Just reached the threshold
                    {
                        OnInputEventOccurred.Invoke();
                        withinThreshold = true;
                    }
                }
                else //Outside threshold
                    withinThreshold = false;
            }
        }
    }
}