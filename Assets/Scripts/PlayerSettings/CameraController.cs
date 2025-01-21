using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSettings
{
    public class CameraController : MonoBehaviour
    {
        public void RotateCamera(Vector2 lookInput, Transform playerTransform, ref float verticalRotation, float sensitivity)
        {
            float mouseX = lookInput.x * sensitivity;
            float mouseY = lookInput.y * sensitivity;

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            playerTransform.Rotate(Vector3.up * mouseX);
        }
    }
}