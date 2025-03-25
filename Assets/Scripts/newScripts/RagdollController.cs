using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public Rigidbody[] ragdollRigidbodies;
    public Animator animator;
    public float ragdollDuration = 2f;

    private void Awake()
    {
        // Automatically find all child rigidbodies (make sure your character’s main Rigidbody is separate)
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        // Initially, you may want child rigidbodies to be kinematic
        SetRagdollState(false);
    }

    public void EnableRagdoll()
    {
        animator.enabled = false;
        SetRagdollState(true);
        Invoke(nameof(DisableRagdoll), ragdollDuration);
    }

    public void DisableRagdoll()
    {
        SetRagdollState(false);
        animator.enabled = true;
        // Optionally, reset the character’s pose or position if needed
    }

    private void SetRagdollState(bool state)
    {
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            // Skip the main rigidbody if needed (or tag them appropriately)
            if (rb.gameObject != gameObject)
            {
                rb.isKinematic = !state;
            }
        }
    }
}
