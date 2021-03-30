using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mfDev.XR.Input;

public class XRInputTest : MonoBehaviour
{
    #region Input Listeners

    public XRButtonInputListener testButtonListener;
    public XRAxisInputListener testAxisListener;
    public XR2DAxisValuedInputListener test2DAxisValuedListener;
    public XR2DAxisDirectionalInputListener test2DAxisDirectionalInputListener;

    #endregion Input Listeners

    #region Manual Subscriptions

    //public XRController controller;
    //XRControllerInputUtility controllerInput;

    #endregion Manual Subscriptions

    private void Start()
    {
        Debug.Log("[mfDev.XR] Initializing XRInputTest");

        #region Input Listeners

        testButtonListener.OnInputEventFired.AddListener(handleTestButton);
        testButtonListener.activate();

        testAxisListener.OnInputEventFired.AddListener(handleTestAxis);
        testAxisListener.activate();

        //test2DAxisValuedListener.OnInputEventFired.AddListener(handleTest2DAxisValue);
        //test2DAxisValuedListener.activate();

        test2DAxisDirectionalInputListener.OnInputEventFired.AddListener(handleTest2DAxisDirectional);
        test2DAxisDirectionalInputListener.activate();

        #endregion Input Listeners

        #region Manual Subscriptions

        //controllerInput = XRInputManager.Instance.getXRControllerInputUtility(controller);

        //if (controllerInput.tryGetXRButtonInput(XRControllerInputFeature.PrimaryButton, out XRButtonInput primaryButtonInput))
        //{
        //    Debug.Log("[mfDev.XR] Adding listeners to primaryButtonInput");
        //    primaryButtonInput.OnButtonPressed.addListener(onPrimaryButtonPressed);
        //    primaryButtonInput.OnButtonReleased.addListener(onPrimaryButtonReleased);
        //}
        //else
        //    Debug.Log("[mfDev.XR] Failed to add listeners to primaryButtonInput");

        //if (controllerInput.tryGetXRAxisInput(XRControllerInputFeature.Trigger, out XRAxisInput triggerInput))
        //{
        //    Debug.Log("[mfDev.XR] Adding listener to triggerInput");
        //    triggerInput.OnChange.addListener(onTriggerChange);
        //}
        //else
        //    Debug.Log("[mfDev.XR] Failed to add listeners to onTriggerChange");

        //if (controllerInput.tryGetXR2DAxisInput(XRControllerInputFeature.Primary2DAxis, out XR2DAxisInput primary2DAxisInput))
        //{
        //    Debug.Log("[mfDev.XR] Adding listeners to primary2DAxisInput");
        //    //primary2DAxisInput.OnChange.addListener(onPrimary2DAxisChange);
        //    primary2DAxisInput.OnAxisLeft.addListener(onPrimary2DAxisLeft);
        //    primary2DAxisInput.OnAxisRight.addListener(onPrimary2DAxisRight);
        //    primary2DAxisInput.OnAxisUp.addListener(onPrimary2DAxisUp);
        //    primary2DAxisInput.OnAxisDown.addListener(onPrimary2DAxisDown);
        //}
        //else
        //    Debug.Log("[mfDev.XR] Failed to add listeners to primary2DAxisInput");

        #endregion Manual Subscriptions

        Debug.Log("[mfDev.XR] Initialization complete");
    }

    #region Input Listener Event Handlers

    public void handleTestButton()
    {
        Debug.Log("[" + testButtonListener.inputSource + "] " + testButtonListener.inputEvent + " fired for: " + testButtonListener.buttonInputFeature);
    }

    public void handleTestAxis(float value)
    {
        Debug.Log("[" + testAxisListener.inputSource + "] " + testAxisListener.inputEvent +
            " fired for: " + testAxisListener.axisInputFeature + " | value: " + value);
    }

    public void handleTest2DAxisValue(Vector2 value)
    {
        Debug.Log("[" + test2DAxisValuedListener.inputSource + "] " + test2DAxisValuedListener.inputEvent +
            " fired for: " + test2DAxisValuedListener.axis2DInputFeature + " | value: " + value);
    }

    public void handleTest2DAxisDirectional()
    {
        Debug.Log("[" + test2DAxisDirectionalInputListener.inputSource + "] " + test2DAxisDirectionalInputListener.inputEvent +
            " fired for: " + test2DAxisDirectionalInputListener.axis2DInputFeature);
    }

    #endregion Input Listener Event Handlers

    #region Manual Subscription Event Handlers

    //public void onPrimaryButtonPressed()
    //{
    //    Debug.Log("[" + controller + "] onPrimaryButtonPressed");
    //}

    //public void onPrimaryButtonReleased()
    //{
    //    Debug.Log("[" + controller + "] onPrimaryButtonReleased");
    //}

    //public void onTriggerChange(float value)
    //{
    //    Debug.Log("[" + controller + "] onTriggerChange to value: " + value);
    //}

    //public void onPrimary2DAxisChange(Vector2 value)
    //{
    //    Debug.Log("[" + controller + "] onPrimary2DAxisChange to value: " + value);
    //}

    //public void onPrimary2DAxisLeft()
    //{
    //    Debug.Log("[" + controller + "] onPrimary2DAxisLeft");
    //}

    //public void onPrimary2DAxisRight()
    //{
    //    Debug.Log("[" + controller + "] onPrimary2DAxisRight");
    //}

    //public void onPrimary2DAxisUp()
    //{
    //    Debug.Log("[" + controller + "] onPrimary2DAxisUp");
    //}

    //public void onPrimary2DAxisDown()
    //{
    //    Debug.Log("[" + controller + "] onPrimary2DAxisDown");
    //}

    #endregion Manual Subscription Event Handlers
}
