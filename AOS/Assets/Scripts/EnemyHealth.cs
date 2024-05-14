using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 60;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle enemy death (e.g., play death animation, remove from scene)
        Destroy(gameObject);
    }
}
