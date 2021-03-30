using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace mfDev.XR
{
    public class XRCameraRig : MonoBehaviour
    {
        private static XRCameraRig instance;
        private static XRCameraRig Instance
        {
            get
            {
                if (!instance)
                    instance = FindObjectOfType<XRCameraRig>();
                return instance;
            }
        }

        [SerializeField, Tooltip("Determines where the tracking origin is for all XR devices.")]
        private TrackingOriginModeFlags trackingOrigin;

        internal XRInputSubsystem XRInputSubsystem
        {
            get
            {
                if (xrInputSubsystem == null)
                    initialize();
                return xrInputSubsystem;
            }
            private set
            {
                xrInputSubsystem = value;
            }
        }
        private XRInputSubsystem xrInputSubsystem;

        private void Awake()
        {
            setTrackingOrigin();
        }

        private void initialize()
        {
            Debug.Log("[mfDev.XR] Initializing XRCameraRig");

            List<XRInputSubsystem> instances = new List<XRInputSubsystem>();
            SubsystemManager.GetInstances(instances);

            if (instances.Count > 0)
            {
                XRInputSubsystem = instances[0];
            }
            else
                Debug.LogError("[mfDev.XR] No XRInputSubsystems found");
        }

        private void setTrackingOrigin()
        {
            if (XRInputSubsystem != null)
            {
                XRInputSubsystem.TrySetTrackingOriginMode(trackingOrigin); //Set tracking origin
                Debug.Log("[mfDev.XR] Set tracking origin to: " + trackingOrigin);
            }
        }
    }
}