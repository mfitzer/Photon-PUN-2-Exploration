using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace mfitzer.Interactions
{
    public class Keyboard : MonoBehaviour
    {
        private static Keyboard instance;
        public static Keyboard Instance
        {
            get
            {
                if (!instance)
                    instance = FindObjectOfType<Keyboard>();
                return instance;
            }
        }

        [SerializeField]
        private InputField inputField;

        [SerializeField]
        private EventSystem eventSystem;

        private const string SHIFT = "↑";
        private const string SYMBOL = "!#1";
        private const string ALTERNATE_SYMBOL = "ABC";
        private const string RETURN = "↵";
        private const string BACK = "←";

        private bool capital = false;

        public void onKeyPress(string value)
        {
            Debug.Log("Key press: " + value);

            //Handle special cases
            switch (value)
            {
                case SHIFT:
                    onShiftPress();
                    return;
                case SYMBOL:
                    onSymbolPress();
                    return;
                case ALTERNATE_SYMBOL:
                    onSymbolPress();
                    return;
                case RETURN:
                    onReturnPress();
                    return;
                case BACK:
                    onBackPress();
                    return;
            }

            if (capital)
                value = value.ToUpper();
            else
                value = value.ToLower();

            setInputFieldText(inputField.text + value);
        }

        private void onShiftPress()
        {
            Debug.Log("Shift key press");
            capital = !capital;
        }

        private void onSymbolPress()
        {
            Debug.Log("Symbol key press");
        }

        private void onReturnPress()
        {
            Debug.Log("Return key press");

            //End the edit on the input field
            eventSystem.SetSelectedGameObject(null);
        }

        private void onBackPress()
        {
            Debug.Log("Back key press");

            setInputFieldText(inputField.text.Remove(inputField.text.Length - 1));
        }

        private void setInputFieldText(string text)
        {
            inputField.text = text;
            inputField.caretPosition = text.Length;
            inputField.ForceLabelUpdate();
        }
    }
}