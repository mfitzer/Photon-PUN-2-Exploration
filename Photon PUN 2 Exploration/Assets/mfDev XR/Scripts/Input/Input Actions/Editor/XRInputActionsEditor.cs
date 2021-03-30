using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace mfDev.XR.Input.Actions
{
    [CustomEditor(typeof(XRInputActions))]
    public class XRInputActionsEditor : Editor
    {
        private const string inputActionsFolderPath = "mfDev XR/Scripts/Input/Input Actions";
        private const string inputActionMgrPath = inputActionsFolderPath + "/XRInputActionManager.cs";
        private const string inputActionBindingsPath = inputActionsFolderPath + "/XRInputActionBindings.cs";

        private const string inputActionsBegin = "//%_INPUT_ACTIONS_BEGIN_%";
        private const string inputActionsEnd = "//%_INPUT_ACTIONS_END_%";

        private readonly Dictionary<InputType, System.Type> unityEventsByInputType = new Dictionary<InputType, System.Type>()
        {
            { InputType.Button, typeof(UnityEvent) },
            { InputType.Axis, typeof(FloatEvent) },
            { InputType.Axis_2D_Value, typeof(Vector2Event) },
            { InputType.Axis_2D_Direction, typeof(UnityEvent) },
        };

        private readonly Dictionary<InputType, System.Type> actionBindingListsByInputType = new Dictionary<InputType, System.Type>()
        {
            { InputType.Button, typeof(XRButtonInputActionBindingList) },
            { InputType.Axis, typeof(XRAxisInputActionBindingList) },
            { InputType.Axis_2D_Value, typeof(XR2DAxisValuedInputActionBindingList) },
            { InputType.Axis_2D_Direction, typeof(XR2DAxisDirectionalInputActionBindingList) },
        };

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            XRInputActions inputActions = (XRInputActions)target;

            EditorGUILayout.Space(10);

            if (GUILayout.Button("Generate Code"))
            {
                generateCode(inputActions);
            }
        }

        private void generateCode(XRInputActions inputActions)
        {
            generateInputActionMgrCode(inputActions.actions);
            generateInputActionBindingCode(inputActions.actions);
        }

        #region XRInputActionManager

        private void generateInputActionMgrCode(List<XRInputAction> actions)
        {
            string generatedCode = "\n";

            //Generate unity events for input actions
            for (int i = 0; i < actions.Count; i++)
            {
                generatedCode += generateActionEventCode(actions[i]);

                if (i < actions.Count - 1)
                    generatedCode += "\n";
            }

            generatedCode += generateActionEventMappingMethod(actions);

            ScriptGenerator.updateGeneratedScript(
                inputActionMgrPath,
                inputActionsBegin,
                inputActionsEnd,
                generatedCode
            );
        }

        //Generate UnityEvent variable for the XRInputAction based on the input type;
        private string generateActionEventCode(XRInputAction action)
        {
            string unityEventType = unityEventsByInputType[action.inputType].Name;

            return "\t\tpublic " + unityEventType + " " + action.name + ";\n" +
                "\t\tinternal static XRInputAction " + getInputActionVarName(action) +
                " { get; } = new XRInputAction(\"" + action.name + "\", InputType." + action.inputType + ");\n";
        }

        //Generate method for configuring a dictionary mapping input actions to unity events
        private string generateActionEventMappingMethod(List<XRInputAction> actions)
        {
            string generatedCode = "\n";

            generatedCode +=
                "\t\tprivate void mapActionEvents()\n" +
                "\t\t{\n" +
                "\t\t\tactionEvents = new Dictionary<XRInputAction, object>()\n" +
                "\t\t\t{\n";
            
            for (int i = 0; i < actions.Count; i++)
            {
                generatedCode +=
                    "\t\t\t\t{ " + getInputActionVarName(actions[i]) + ", " + actions[i].name + " }";

                if (i < actions.Count - 1)
                    generatedCode += ",\n";
            }

            generatedCode +=
                "\n\t\t\t};\n" +
                "\t\t}\n";                

            return generatedCode;
        }

        #endregion XRInputActionManager

        #region XRInputActionBindings

        private void generateInputActionBindingCode(List<XRInputAction> actions)
        {
            string generatedCode = "\n";

            generatedCode += generateActionBindingListCode(actions);

            generatedCode += "\n";

            ScriptGenerator.updateGeneratedScript(
                inputActionBindingsPath,
                inputActionsBegin,
                inputActionsEnd,
                generatedCode
            );
        }

        private string generateActionBindingListCode(List<XRInputAction> actions)
        {
            string generatedCode = "";

            for (int i = 0; i < actions.Count; i++)
            {
                string actionBindingListName = actionBindingListsByInputType[actions[i].inputType].Name;

                generatedCode += "\t\tpublic " + actionBindingListName + " " + actions[i].name + "InputBindings = " +
                    "new " + actionBindingListName + "(XRInputActionManager." + getInputActionVarName(actions[i]) + ");";

                if (i < actions.Count - 1)
                    generatedCode += "\n";
            }

            generatedCode += "\n\n\t\tpublic XRInputActionBindingList getInputActionBindingList(XRInputAction action)\n" +
                "\t\t{\n" +
                "\t\t\tif (inputActionBindingLists == null)" +
                "\t\t\t{\n" +
                "\t\t\t\tinputActionBindingLists = new Dictionary<XRInputAction, XRInputActionBindingList>()\n" +
                "\t\t\t\t{\n";

            for (int i = 0; i < actions.Count; i++)
            {
                string actionBindingListVarName = actions[i].name + "InputBindings";

                generatedCode += "\t\t\t\t\t{ " + actionBindingListVarName + ".InputAction, " + actionBindingListVarName + " }";

                if (i < actions.Count - 1)
                    generatedCode += ",\n";
            }

            generatedCode += "\n\t\t\t\t};\n" +
                "\t\t\t}\n\n" +
                "\t\t\treturn inputActionBindingLists[action];\n" +
                "\t\t}";

            return generatedCode;
        }

        #endregion XRInputActionBindings

        //Get the name of an input action variable
        private string getInputActionVarName(XRInputAction action)
        {
            return action.name + "Action";
        }
    }
}
