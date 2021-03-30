using System;
using System.Collections.Generic;
using UnityEngine;

namespace mfDev.XR.Input.Actions
{
    [CreateAssetMenu(fileName = "new XRInputActions", menuName = "mfDev XR/XRInputActions", order = 1)]
    public class XRInputActions : ScriptableObject
    {
        [SerializeField]
        public List<XRInputAction> actions;

        [SerializeField, HideInInspector]
        private List<string> actionNames = new List<string>();

        private void OnValidate()
        {
            //Ensure action names are valid for use as variable names and are unique
            actionNames.Clear();
            for (int i = 0; i < actions.Count; i++)
            {
                string actionName = actions[i].name;

                //Make sure action name does not start with a digit (0 - 9)
                if (char.IsDigit(actionName[0]))
                    actionName = actionName.Insert(0, "_");

                //Remove any invalid characters
                for (int j = 0; j < actionName.Length;)
                {
                    if (!char.IsLetterOrDigit(actionName[j]) && actionName[j] != '_')
                        actionName = actionName.Replace(actionName[j].ToString(), "");
                    else
                        j++;
                }
                
                if (actionNames.Contains(actionName))//Action name is not unique
                {
                    int actionNameDuplicateId = 0;
                    string uniqueActionName;

                    do
                    {
                        uniqueActionName = actionName + ++actionNameDuplicateId;
                    }
                    while (actionNames.Contains(uniqueActionName));

                    actionName = uniqueActionName;
                }

                //Record name to prevent duplicates
                actionNames.Add(actionName);

                //Set edited action name
                actions[i].name = actionName;
            }
        }
    }

    [Serializable]
    public class XRInputAction : IEquatable<XRInputAction>
    {
        public string name;
        public InputType inputType;

        public XRInputAction(string name, InputType inputType)
        {
            this.name = name;
            this.inputType = inputType;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is XRInputAction))
                return false;

            XRInputAction inputAction = (XRInputAction)obj;

            return Equals(inputAction);
        }

        public bool Equals(XRInputAction other)
        {
            return other != null &&
                   name == other.name &&
                   inputType == other.inputType;
        }

        public override int GetHashCode()
        {
            var hashCode = 1044888089;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + inputType.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(XRInputAction action1, XRInputAction action2)
        {
            return EqualityComparer<XRInputAction>.Default.Equals(action1, action2);
        }

        public static bool operator !=(XRInputAction action1, XRInputAction action2)
        {
            return !(action1 == action2);
        }
    }

    public enum InputType
    {
        Button,
        Axis,
        Axis_2D_Value,
        Axis_2D_Direction
    }
}