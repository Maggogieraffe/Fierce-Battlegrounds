using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float dashSpeed = 12f;
    public float dashDuration = 0.2f;
    public float jumpForce = 8f;

    [Header("Shift Lock Settings")]
    public bool shiftLockEnabled = false;

    [Header("References")]
    public Transform cameraTransform; // Assign your main camera here (or leave null to auto-assign)

    private Rigidbody rb;
    private bool isDashing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        // Get input axes
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Calculate movement direction relative to the camera
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0;
        camForward.Normalize();
        Vector3 camRight = cameraTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveDirection = (camRight * horizontal + camForward * vertical).normalized;

        // If shift lock is enabled, smoothly rotate the player to face camera direction
        if (shiftLockEnabled)
        {
            Quaternion targetRotation = Quaternion.LookRotation(camForward);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        else if (moveDirection != Vector3.zero)
        {
            // Otherwise, face movement direction
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * 10f);
        }

        // Apply movement if not dashing
        if (!isDashing)
        {
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
        }

        // Dash – press LeftShift to dash in the direction of input (default forward if none)
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash(moveDirection));
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    IEnumerator Dash(Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            // Default to forward dash relative to camera if no movement input
            direction = cameraTransform.forward;
            direction.y = 0;
            direction.Normalize();
        }
        isDashing = true;
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            rb.MovePosition(transform.position + direction * dashSpeed * Time.deltaTime);
            yield return null;
        }
        isDashing = false;
    }

    private bool IsGrounded()
    {
        // Simple raycast ground check – adjust the distance as needed
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
