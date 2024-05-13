using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;            // Speed at which the enemy moves
    public Transform player;                // Reference to the player
    public float attackRange = 2f;          // Range at which the enemy attacks
    public float detectionRange = 5f;       // Range at which the enemy detects the player

    private bool movingRight = true;        // Flag to track movement direction

    void Update()
    {
        Move();                             // Move the enemy
        CheckForPlayer();                   // Check if player is in sight
    }

    void Move()
    {
        // Define movement direction
        Vector2 movement = Vector2.right * (movingRight ? 1 : -1);

        // Move the enemy
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If collided with an object tagged as "Wall", flip the direction
        if (collision.gameObject.CompareTag("Wall"))
        {
            movingRight = !movingRight;
            Flip();
        }
    }

    void CheckForPlayer()
    {
        // Check if player is within detection range
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            // Move towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // If within attack range, attack the player
            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                AttackPlayer();
            }
        }
    }

    void Flip()
    {
        // Flip the enemy's direction
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void AttackPlayer()
    {
        // Add attack logic here
        Debug.Log("Attacking Player!");
    }
}
