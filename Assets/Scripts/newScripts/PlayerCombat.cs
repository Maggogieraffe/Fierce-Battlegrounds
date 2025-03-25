using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackResetTime = 0.5f;
    public float[] attackDamages = new float[4] { 10f, 12f, 14f, 20f };

    private int currentAttack = 0;
    private float lastAttackTime = 0f;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Special abilities (keys 1 through 4)
        if (Input.GetKeyDown(KeyCode.Alpha1)) { UseSpecialAbility(1); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { UseSpecialAbility(2); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { UseSpecialAbility(3); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { UseSpecialAbility(4); }

        // Attack input
        if (Input.GetMouseButtonDown(0))
        {
            // Reset chain if too much time has passed
            if (Time.time - lastAttackTime > attackResetTime)
            {
                currentAttack = 0;
            }
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        // Trigger the appropriate attack animation
        animator.SetTrigger("Attack" + (currentAttack + 1));

        // Here you would add your logic to detect and damage the target.
        // For example: target.GetComponent<PlayerHealth>()?.ApplyDamage(attackDamages[currentAttack]);
        // Or call a method on a hit collider.

        // On the fourth attack (finisher), decide what finishing move to apply.
        if (currentAttack == 3)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                // Throw the enemy upward
                // (You can call a method on the enemy’s movement or ragdoll controller)
                Debug.Log("Finisher: Throw enemy up!");
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                // Alternatively, if you want jump+click for slam
                Debug.Log("Finisher: Slam enemy into the ground!");
            }
            else
            {
                // Regular finisher that triggers ragdoll
                Debug.Log("Finisher: Ragdoll enemy!");
            }
            // Reset the chain after the finisher
            currentAttack = 0;
        }
        else
        {
            currentAttack++;
        }
    }

    void UseSpecialAbility(int abilityNumber)
    {
        // Stub for special ability attacks (keys 1-4)
        animator.SetTrigger("Special" + abilityNumber);
        Debug.Log("Used Special Ability " + abilityNumber);
        // Insert additional logic (damage, effects) here
    }
}
