using UnityEngine;

namespace mfitzer.Networking
{
    public class TransformAliasController : MonoBehaviour
    {
        public Transform alias;

        private void Update()
        {
            if (alias)
            {
                alias.transform.position = transform.position;
                alias.transform.rotation = transform.rotation;
            }
        }
    }
}
