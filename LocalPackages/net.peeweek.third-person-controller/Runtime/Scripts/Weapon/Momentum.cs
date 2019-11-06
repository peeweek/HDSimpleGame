using UnityEngine;

namespace ThirdPersonController
{
    [RequireComponent(typeof(Rigidbody))]
    public class Momentum : MonoBehaviour
    {
        public Vector3 LocalMomentum = Vector3.one * 10;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Rigidbody>().AddForce(transform.localToWorldMatrix * LocalMomentum, ForceMode.VelocityChange);
        }
    }
}
