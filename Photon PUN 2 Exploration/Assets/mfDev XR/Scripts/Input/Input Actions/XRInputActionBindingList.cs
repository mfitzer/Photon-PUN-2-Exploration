using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mfDev.XR.Input.Actions
{
    [System.Serializable]
    public class XRInputActionBindingList
    {
        [SerializeField, HideInInspector]
        private XRInputAction inputAction;
        public XRInputAction InputAction
        {
            get
            {
                return inputAction;
            }
        }

        public XRInputActionBindingList(XRInputAction inputAction)
        {
            this.inputAction = inputAction;
        }
    }

    [System.Serializable]
    public class XRButtonInputActionBindingList : XRInputActionBindingList
    {
        public XRButtonInputActionBindingList(XRInputAction inputAction)
            : base(inputAction) { }

        [SerializeField]
        private List<XRButtonInputActionBinding> inputActionBindings;

        public XRButtonInputActionBinding[] InputActionBindings
        {
            get
            {
                return inputActionBindings.ToArray();
            }
        }
    }

    [System.Serializable]
    public class XRAxisInputActionBindingList : XRInputActionBindingList
    {
        public XRAxisInputActionBindingList(XRInputAction inputAction)
            : base(inputAction) { }

        [SerializeField]
        private List<XRAxisInputActionBinding> inputActionBindings;

        public XRAxisInputActionBinding[] InputActionBindings
        {
            get
            {
                return inputActionBindings.ToArray();
            }
        }
    }

    [System.Serializable]
    public class XR2DAxisValuedInputActionBindingList : XRInputActionBindingList
    {
        public XR2DAxisValuedInputActionBindingList(XRInputAction inputAction)
            : base(inputAction) { }

        [SerializeField]
        private List<XR2DAxisValuedInputActionBinding> inputActionBindings;

        public XR2DAxisValuedInputActionBinding[] InputActionBindings
        {
            get
            {
                return inputActionBindings.ToArray();
            }
        }
    }

    [System.Serializable]
    public class XR2DAxisDirectionalInputActionBindingList : XRInputActionBindingList
    {
        public XR2DAxisDirectionalInputActionBindingList(XRInputAction inputAction)
            : base(inputAction) { }

        [SerializeField]
        private List<XR2DAxisDirectionalInputActionBinding> inputActionBindings;

        public XR2DAxisDirectionalInputActionBinding[] InputActionBindings
        {
            get
            {
                return inputActionBindings.ToArray();
            }
        }
    }
}