using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace mfDev.XR.Input.Actions
{
    public class XRInputActionManager : MonoBehaviour
    {
        private static XRInputActionManager instance;
        public static XRInputActionManager Instance
        {
            get
            {
                if (!instance)
                    instance = FindObjectOfType<XRInputActionManager>();
                return instance;
            }
        }

        [SerializeField, Tooltip("Determines which input bindings will be mapped to the input actions.")]
        private XRInputActionBindings inputActionBindings;

        [SerializeField, Tooltip("Determines if the input action listeners will be activated on start.")]
        private bool activateOnStart = true;

        private Dictionary<XRInputAction, object> actionEvents;
        private Dictionary<XRInputAction, List<XRInputListener>> inputActionListeners;

//%_INPUT_ACTIONS_BEGIN_%

		public UnityEvent LeftGrab;
		internal static XRInputAction LeftGrabAction { get; } = new XRInputAction("LeftGrab", InputType.Button);

		public UnityEvent LeftRelease;
		internal static XRInputAction LeftReleaseAction { get; } = new XRInputAction("LeftRelease", InputType.Button);

		public UnityEvent RightGrab;
		internal static XRInputAction RightGrabAction { get; } = new XRInputAction("RightGrab", InputType.Button);

		public UnityEvent RightRelease;
		internal static XRInputAction RightReleaseAction { get; } = new XRInputAction("RightRelease", InputType.Button);

		public UnityEvent ToggleLeftRayInteractor;
		internal static XRInputAction ToggleLeftRayInteractorAction { get; } = new XRInputAction("ToggleLeftRayInteractor", InputType.Button);

		public UnityEvent ToggleRightRayInteractor;
		internal static XRInputAction ToggleRightRayInteractorAction { get; } = new XRInputAction("ToggleRightRayInteractor", InputType.Button);

		private void mapActionEvents()
		{
			actionEvents = new Dictionary<XRInputAction, object>()
			{
				{ LeftGrabAction, LeftGrab },
				{ LeftReleaseAction, LeftRelease },
				{ RightGrabAction, RightGrab },
				{ RightReleaseAction, RightRelease },
				{ ToggleLeftRayInteractorAction, ToggleLeftRayInteractor },
				{ ToggleRightRayInteractorAction, ToggleRightRayInteractor }
			};
		}

//%_INPUT_ACTIONS_END_%

        private void Start()
        {
            mapActionEvents();
            initialize();

            if (activateOnStart)
                activate();
        }

        private void initialize()
        {
            inputActionListeners = new Dictionary<XRInputAction, List<XRInputListener>>();

            foreach (KeyValuePair<XRInputAction, object> actionEvent in actionEvents)
            {
                XRInputAction action = actionEvent.Key;
                XRInputActionBindingList bindings = inputActionBindings.getInputActionBindingList(action);
                List<XRInputListener> listeners = null;

                switch (action.inputType)
                {
                    case InputType.Button:
                        listeners = configureButtonListeners(action, bindings, actionEvent.Value);
                        break;
                    case InputType.Axis:
                        listeners = configureAxisListeners(action, bindings, actionEvent.Value);
                        break;
                    case InputType.Axis_2D_Value:
                        listeners = configure2DAxisValuedListeners(action, bindings, actionEvent.Value);
                        break;
                    case InputType.Axis_2D_Direction:
                        listeners = configure2DAxisDirectionalListeners(action, bindings, actionEvent.Value);
                        break;
                }

                inputActionListeners.Add(action, listeners);
            }
        }

        private List<XRInputListener> configureButtonListeners(XRInputAction action, XRInputActionBindingList bindings, object eventObj)
        {
            UnityEvent actionEvent = (UnityEvent)eventObj;
            List<XRInputListener> inputListeners = new List<XRInputListener>();
            XRButtonInputActionBindingList buttonActionBindings = (XRButtonInputActionBindingList)bindings;

            //Configure input listeners for each input action binding
            foreach (XRButtonInputActionBinding actionBinding in buttonActionBindings.InputActionBindings)
            {
                XRButtonInputListener listener = new XRButtonInputListener(actionBinding.inputBinding, actionBinding.inputSource);

                //Subscribe action event to event listener
                listener.OnInputEventFired.AddListener(actionEvent.Invoke);

                inputListeners.Add(listener);
            }

            return inputListeners;
        }

        private List<XRInputListener> configureAxisListeners(XRInputAction action, XRInputActionBindingList bindings, object eventObj)
        {
            FloatEvent actionEvent = (FloatEvent)eventObj;
            List<XRInputListener> inputListeners = new List<XRInputListener>();
            XRAxisInputActionBindingList actionBindings = (XRAxisInputActionBindingList)bindings;

            //Configure input listeners for each input action binding
            foreach (XRAxisInputActionBinding actionBinding in actionBindings.InputActionBindings)
            {
                XRAxisInputListener listener = new XRAxisInputListener(actionBinding.inputBinding, actionBinding.inputSource);

                //Subscribe action event to event listener
                listener.OnInputEventFired.AddListener(actionEvent.Invoke);

                inputListeners.Add(listener);
            }

            return inputListeners;
        }

        private List<XRInputListener> configure2DAxisValuedListeners(XRInputAction action, XRInputActionBindingList bindings, object eventObj)
        {
            Vector2Event actionEvent = (Vector2Event)eventObj;
            List<XRInputListener> inputListeners = new List<XRInputListener>();
            XR2DAxisValuedInputActionBindingList actionBindings = (XR2DAxisValuedInputActionBindingList)bindings;

            //Configure input listeners for each input action binding
            foreach (XR2DAxisValuedInputActionBinding actionBinding in actionBindings.InputActionBindings)
            {
                XR2DAxisValuedInputListener listener = new XR2DAxisValuedInputListener(actionBinding.inputBinding, actionBinding.inputSource);

                //Subscribe action event to event listener
                listener.OnInputEventFired.AddListener(actionEvent.Invoke);

                inputListeners.Add(listener);
            }

            return inputListeners;
        }

        private List<XRInputListener> configure2DAxisDirectionalListeners(XRInputAction action, XRInputActionBindingList bindings, object eventObj)
        {
            UnityEvent actionEvent = (UnityEvent)eventObj;
            List<XRInputListener> inputListeners = new List<XRInputListener>();
            XR2DAxisDirectionalInputActionBindingList actionBindings = (XR2DAxisDirectionalInputActionBindingList)bindings;

            //Configure input listeners for each input action binding
            foreach (XR2DAxisDirectionalInputActionBinding actionBinding in actionBindings.InputActionBindings)
            {
                XR2DAxisDirectionalInputListener listener = new XR2DAxisDirectionalInputListener(actionBinding.inputBinding, actionBinding.inputSource);

                //Subscribe action event to event listener
                listener.OnInputEventFired.AddListener(actionEvent.Invoke);

                inputListeners.Add(listener);
            }

            return inputListeners;
        }

        private void activateActionListeners()
        {
            if (inputActionListeners != null)
                foreach (List<XRInputListener> actionListeners in inputActionListeners.Values)
                    foreach (XRInputListener listener in actionListeners)
                        listener.activate();
        }

        private void deactivateActionListeners()
        {
            if (inputActionListeners != null)
                foreach (List<XRInputListener> actionListeners in inputActionListeners.Values)
                    foreach (XRInputListener listener in actionListeners)
                        listener.deactivate();
        }

        public void activate()
        {
            activateActionListeners();
        }

        public void deactivate()
        {
            deactivateActionListeners();
        }

        private void OnEnable()
        {
            activate();
        }

        private void OnDisable()
        {
            deactivate();
        }
    }
}