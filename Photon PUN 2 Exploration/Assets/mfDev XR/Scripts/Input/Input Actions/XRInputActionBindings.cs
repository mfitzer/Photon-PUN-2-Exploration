using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mfDev.XR.Input.Actions
{
    [CreateAssetMenu(fileName = "new XRInputActionBindings", menuName = "mfDev XR/XRInputActionBindings", order = 1)]
    public class XRInputActionBindings : ScriptableObject
    {
        private Dictionary<XRInputAction, XRInputActionBindingList> inputActionBindingLists;

//%_INPUT_ACTIONS_BEGIN_%

		public XRButtonInputActionBindingList LeftGrabInputBindings = new XRButtonInputActionBindingList(XRInputActionManager.LeftGrabAction);
		public XRButtonInputActionBindingList LeftReleaseInputBindings = new XRButtonInputActionBindingList(XRInputActionManager.LeftReleaseAction);
		public XRButtonInputActionBindingList RightGrabInputBindings = new XRButtonInputActionBindingList(XRInputActionManager.RightGrabAction);
		public XRButtonInputActionBindingList RightReleaseInputBindings = new XRButtonInputActionBindingList(XRInputActionManager.RightReleaseAction);
		public XRButtonInputActionBindingList ToggleLeftRayInteractorInputBindings = new XRButtonInputActionBindingList(XRInputActionManager.ToggleLeftRayInteractorAction);
		public XRButtonInputActionBindingList ToggleRightRayInteractorInputBindings = new XRButtonInputActionBindingList(XRInputActionManager.ToggleRightRayInteractorAction);

		public XRInputActionBindingList getInputActionBindingList(XRInputAction action)
		{
			if (inputActionBindingLists == null)			{
				inputActionBindingLists = new Dictionary<XRInputAction, XRInputActionBindingList>()
				{
					{ LeftGrabInputBindings.InputAction, LeftGrabInputBindings },
					{ LeftReleaseInputBindings.InputAction, LeftReleaseInputBindings },
					{ RightGrabInputBindings.InputAction, RightGrabInputBindings },
					{ RightReleaseInputBindings.InputAction, RightReleaseInputBindings },
					{ ToggleLeftRayInteractorInputBindings.InputAction, ToggleLeftRayInteractorInputBindings },
					{ ToggleRightRayInteractorInputBindings.InputAction, ToggleRightRayInteractorInputBindings }
				};
			}

			return inputActionBindingLists[action];
		}

//%_INPUT_ACTIONS_END_%
    }
}