using UnityEngine;

namespace WinterSym
{
    public class SimpleCamera : MonoBehaviour
    {
        public Transform playerCamera;
        private Vector2 currentMouseDelta = Vector2.zero;
        private Vector2 currentMouseDelta_Velocity = Vector2.zero;
    
        public float mouseSensitivity = 3.5f;
        [Range(0.0f, 0.5f)] public float mouseSmoothing = 0.03f;

        public float cameraPitch { get; private set; } = 0.0f;
        public float cameraYaw { get; private set; } = 0.0f;

        void Update()
        {
            MoveCamera();
        }

        public void MoveCamera()
        {
            //get the location (x, y) of the mouse
            Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDelta_Velocity, mouseSmoothing);

            //rotate on the x axis up/down, within range (-75, 75)
            cameraPitch -= currentMouseDelta.y * mouseSensitivity;
            cameraPitch = Mathf.Clamp(cameraPitch, -75, 75);
            playerCamera.localEulerAngles = Vector3.right * cameraPitch;

            cameraYaw += currentMouseDelta.x * mouseSensitivity;

            // Debug.Log(Vector3.up * cameraYaw);

            transform.localEulerAngles = Vector3.up * cameraYaw;
        }
    }
}
