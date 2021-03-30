using UnityEngine;

namespace mfitzer.Interactions
{
    public class RayInteractorManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject leftRayInteractor;

        [SerializeField]
        private GameObject rightRayInteractor;

        public void toggleLeftRayInteractor()
        {
            Debug.Log("Toggle left ray interactor");
            leftRayInteractor.SetActive(!leftRayInteractor.activeSelf);
        }

        public void toggleRightRayInteractor()
        {
            Debug.Log("Toggle right ray interactor");
            rightRayInteractor.SetActive(!rightRayInteractor.activeSelf);
        }
    }
}