using System;
using System.Collections.Generic;
using UnityEngine;

namespace mfDev.XR.Input.Actions
{
    [Serializable]
    public class XRInputActionBinding<T> : IEquatable<XRInputActionBinding<T>> where T : XRInputBinding
    {
        [HideInInspector]
        public XRInputAction inputAction;
        public XRController inputSource;
        public T inputBinding;

        public XRInputActionBinding(XRInputAction inputAction, XRController inputSource, T inputBinding)
        {
            this.inputAction = inputAction;
            this.inputSource = inputSource;
            this.inputBinding = inputBinding;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is XRInputActionBinding<T>))
                return false;

            XRInputActionBinding<T> inputActionBinding = (XRInputActionBinding<T>)obj;

            return inputActionBinding.inputAction == inputAction && 
                inputActionBinding.inputSource == inputSource &&
                inputActionBinding.inputBinding == inputBinding;
        }

        public bool Equals(XRInputActionBinding<T> other)
        {
            return other != null &&
                   EqualityComparer<XRInputAction>.Default.Equals(inputAction, other.inputAction) &&
                   inputSource == other.inputSource &&
                   EqualityComparer<T>.Default.Equals(inputBinding, other.inputBinding);
        }

        public override int GetHashCode()
        {
            var hashCode = 766329052;
            hashCode = hashCode * -1521134295 + EqualityComparer<XRInputAction>.Default.GetHashCode(inputAction);
            hashCode = hashCode * -1521134295 + inputSource.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(inputBinding);
            return hashCode;
        }

        public static bool operator ==(XRInputActionBinding<T> binding1, XRInputActionBinding<T> binding2)
        {
            return EqualityComparer<XRInputActionBinding<T>>.Default.Equals(binding1, binding2);
        }

        public static bool operator !=(XRInputActionBinding<T> binding1, XRInputActionBinding<T> binding2)
        {
            return !(binding1 == binding2);
        }
    }

    [Serializable]
    public class XRButtonInputActionBinding : XRInputActionBinding<XRButtonInputBinding>
    {
        public XRButtonInputActionBinding(XRInputAction inputAction, XRController controller, XRButtonInputBinding xrInputBinding)
            : base(inputAction, controller, xrInputBinding) { }
    }

    [Serializable]
    public class XRAxisInputActionBinding : XRInputActionBinding<XRAxisInputBinding>
    {
        public XRAxisInputActionBinding(XRInputAction inputAction, XRController controller, XRAxisInputBinding xrInputBinding)
            : base(inputAction, controller, xrInputBinding) { }
    }

    [Serializable]
    public class XR2DAxisValuedInputActionBinding : XRInputActionBinding<XR2DAxisValuedInputBinding>
    {
        public XR2DAxisValuedInputActionBinding(XRInputAction inputAction, XRController controller, XR2DAxisValuedInputBinding xrInputBinding)
            : base(inputAction, controller, xrInputBinding) { }
    }

    [Serializable]
    public class XR2DAxisDirectionalInputActionBinding : XRInputActionBinding<XR2DAxisDirectionalInputBinding>
    {
        public XR2DAxisDirectionalInputActionBinding(XRInputAction inputAction, XRController controller, XR2DAxisDirectionalInputBinding xrInputBinding)
            : base(inputAction, controller, xrInputBinding) { }
    }
}