using UnityEngine;

namespace WinterSym
{
    public class VirtualFloor : MonoBehaviour
    {
        public bool isGrounded = true;
        public LayerMask layerMask;

        public bool IsGrounded()
        {
            return isGrounded;
        }

        void OnTriggerStay(Collider other)
        {
            Debug.Log("VirtualFloor: " + "Is grounded!");
            isGrounded = true;
        }

        void OnTriggerExit(Collider other)
        {
            Debug.Log("VirtualFloor: " + "Is not grounded!");
            isGrounded = false;
        }
    }
}


