using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.UI;

namespace mfitzer.Interactions
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(GraphicRaycaster))]
    [RequireComponent(typeof(TrackedDeviceGraphicRaycaster))]
    public class XRUIInputConfigurer : MonoBehaviour
    {
        #region Canvas Config

        [SerializeField, ReadOnlyField]
        private GraphicRaycaster graphicRaycaster;

        [SerializeField, ReadOnlyField]
        private TrackedDeviceGraphicRaycaster xrGraphicRaycaster;

        #endregion Canvas Config

        #region Event System Config

        [SerializeField]
        private EventSystem eventSystem;

        [SerializeField, ReadOnlyField]
        private InputSystemUIInputModule uiInputModule;

        [SerializeField, ReadOnlyField]
        private XRUIInputModule xrUIInputModule;

        #endregion

        private void OnValidate()
        {
            getReferences();
        }

        private void Start()
        {
            if (!eventSystem) //EventSystem not set
            {
                eventSystem = FindObjectOfType<EventSystem>();
                if (!eventSystem) //No EventSystem in scene, create one
                {
                    eventSystem = new GameObject("EventSystem").AddComponent<EventSystem>();
                }
            }

            getReferences();
            configure();
        }

        private void getReferences()
        {
            if (!graphicRaycaster)
                graphicRaycaster = GetComponent<GraphicRaycaster>();
            if (!xrGraphicRaycaster)
                xrGraphicRaycaster = GetComponent<TrackedDeviceGraphicRaycaster>();

            if (eventSystem && !uiInputModule)
            {
                uiInputModule = eventSystem.GetComponent<InputSystemUIInputModule>();
                if (!uiInputModule)
                    uiInputModule = eventSystem.gameObject.AddComponent<InputSystemUIInputModule>();
            }
            if (eventSystem && !xrUIInputModule)
            {
                xrUIInputModule = eventSystem.GetComponent<XRUIInputModule>();
                if (!xrUIInputModule)
                    xrUIInputModule = eventSystem.gameObject.AddComponent<XRUIInputModule>();
            }
        }

        private void configure()
        {
            graphicRaycaster.enabled = !XRSettings.enabled;
            xrGraphicRaycaster.enabled = XRSettings.enabled;

            uiInputModule.enabled = !XRSettings.enabled;
            xrUIInputModule.enabled = XRSettings.enabled;
        }
    }
}
