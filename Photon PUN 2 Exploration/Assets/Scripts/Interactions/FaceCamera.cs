using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mfitzer.Interactions
{
    public class FaceCamera : MonoBehaviour
    {
        private Transform mainCam;

        [SerializeField]
        private Vector3 rotationOffset = Vector3.zero;

        private void Start()
        {
            mainCam = Camera.main?.transform;
        }

        private void Update()
        {
            if (!mainCam)
                mainCam = Camera.main?.transform;

            if (mainCam)
            {


                Vector3 toCam = mainCam.position - transform.position;
                Vector3 toCamRot = Quaternion.FromToRotation(Vector3.forward, toCam).eulerAngles;
                transform.rotation = Quaternion.Euler(rotationOffset) * Quaternion.Euler(0f, toCamRot.y, 0f);
            }
        }
    }
}