using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // The player transform
    public Vector3 offset = new Vector3(0, 3, -6);
    public float rotationSpeed = 5f;
    public bool shiftLockEnabled = false;

    private float mouseX;
    private float mouseY;

    private void LateUpdate()
    {
        // Follow the target
        transform.position = target.position + offset;

        if (!shiftLockEnabled)
        {
            // Free camera rotation
            mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
            mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            mouseY = Mathf.Clamp(mouseY, -80, 80);
            transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else
        {
            // In shift lock, the camera aligns with the target’s forward direction
            transform.rotation = Quaternion.LookRotation(target.forward);
        }
    }
}
