using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mfitzer.Interactions
{
    public class WristInterfaceController : MonoBehaviour
    {
        [SerializeField]
        private GameObject wristInterface;

        [SerializeField, Tooltip("How many degrees from facing the camera directly the wrist must be to show its interface.")]
        private float visibilityAngle = 20f;

        private Transform mainCam;

        private void Start()
        {
            mainCam = Camera.main.transform;
        }

        private void Update()
        {
            controlVisibility();
        }

        private void controlVisibility()
        {
            Vector3 toCam = mainCam.position - transform.position;
            float angleToCam = Vector3.Angle(transform.right, toCam);

            wristInterface.SetActive(angleToCam <= visibilityAngle);
        }
    }
}