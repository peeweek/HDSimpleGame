using Cinemachine;
using UnityEngine;

namespace ThirdPersonController
{
    public static class PlayerInput
    {
        private static float lookAngle = 0f;
        private static float tiltAngle = 0f;

        public static Vector3 GetMovementInput(CinemachineVirtualCameraBase relativeCamera)
        {
            Vector3 moveVector;
            float horizontalAxis = Input.GetAxis("Horizontal");
            float verticalAxis = Input.GetAxis("Vertical");

            if (relativeCamera != null)
            {
                // Calculate the move vector relative to camera rotation

                Vector3 cameraForward = (relativeCamera.LookAt.position - relativeCamera.transform.position);
                cameraForward = new Vector3(cameraForward.x, 0, cameraForward.z).normalized; // Get Horizontal Direction (Normalized)

                Vector3 cameraRight = Vector3.Cross( relativeCamera.transform.up, cameraForward);
                moveVector = (cameraForward * verticalAxis + cameraRight * horizontalAxis);
            }
            else
            {
                // Use world relative directions
                Debug.LogWarning("PlayerInput.GetMovementInput : No Relative Camera Found.");
                moveVector = (Vector3.forward * verticalAxis + Vector3.right * horizontalAxis);
            }

            if (moveVector.magnitude > 1f)
            {
                moveVector.Normalize();
            }

            return moveVector;
        }

        public static Quaternion GetMouseRotationInput(float mouseSensitivity = 3f, float minTiltAngle = -75f, float maxTiltAngle = 45f)
        {
            //if (!Input.GetMouseButton(1))
            //{
            //    return;
            //}

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Adjust the look angle (Y Rotation)
            lookAngle += mouseX * mouseSensitivity;
            lookAngle %= 360f;

            // Adjust the tilt angle (X Rotation)
            tiltAngle += mouseY * mouseSensitivity;
            tiltAngle %= 360f;
            tiltAngle = MathfExtensions.ClampAngle(tiltAngle, minTiltAngle, maxTiltAngle);

            var controlRotation = Quaternion.Euler(-tiltAngle, lookAngle, 0f);
            return controlRotation;
        }

        public static bool GetSprintInput()
        {
            return Input.GetButton("Sprint");
        }

        public static bool GetJumpInput()
        {
            return Input.GetButtonDown("Jump");
        }

        public static bool GetToggleWalkInput()
        {
            return Input.GetButtonDown("Toggle Walk");
        }
    }
}
