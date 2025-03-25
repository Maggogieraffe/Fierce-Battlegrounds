using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public RagdollController ragdollController;

    private void Awake()
    {
        currentHealth = maxHealth;
        if (ragdollController == null)
        {
            ragdollController = GetComponent<RagdollController>();
        }
    }

    public void ApplyDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Trigger ragdoll and any death effects
        ragdollController.EnableRagdoll();
        Debug.Log("Player died – ragdoll enabled.");
    }
}
