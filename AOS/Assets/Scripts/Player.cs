using UnityEngine;
using System.Collections; // Add this line to include the System.Collections namespace

public class Player : MonoBehaviour
{
    public float moveSpeed = 3f;            // Speed of the player's movement
    public float jumpForce = 5f;            // Force applied when jumping
    public int health = 100;                // Player's health
    public Rigidbody2D rb;                  // Reference to the player's Rigidbody2D component
    public Animator animator;               // Reference to the player's Animator component

    private bool canJump = true;            // Flag to determine if the player can jump
    private bool canTakeDamage = true;      // Flag to determine if the player can take damage

    // Spells
    public GameObject[] spells;             // Array of spell prefabs
    private SpellShoot spellShoot;          // Reference to the SpellShoot component

    void Start()
    {
        // Ignore collisions with walls
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls)
        {
            Collider2D wallCollider = wall.GetComponent<Collider2D>();
            if (wallCollider != null)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), wallCollider, true);
            }
        }

        // Get reference to the SpellShoot component
        spellShoot = GetComponent<SpellShoot>();
    }

    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // Update animator parameters
        UpdateAnimatorParameters(horizontalInput);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
        }

        // Spell casting
        for (int i = 0; i < spells.Length; i++)
        {
            // Using number keys 1-9 and 0 to select spells
            if (Input.GetKeyDown(KeyCode.Alpha0) && i == 9) // Check for key "0" for spell 10
            {
                CastSpell(i);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // Check for keys 1-9 for spells 1-9
            {
                CastSpell(i);
            }
        }
    }

    void Jump()
    {
        // Apply jump force
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        canJump = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for ground collision to enable jumping
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }

        // Check for enemy collision to take damage
        if (collision.gameObject.CompareTag("enemy") && canTakeDamage)
        {
            TakeDamage(1); // Player takes 1 damage
            StartCoroutine(DamageCooldown());
        }
    }

    void CastSpell(int spellIndex)
    {
        // Call the ShootSpell method from the SpellShoot component
        spellShoot.ShootSpell(spells[spellIndex]);
    }

    void UpdateAnimatorParameters(float horizontalInput)
    {
        // Update animator parameters based on movement direction
        if (horizontalInput < 0)
        {
            animator.SetInteger("Direction", -1); // Moving left
        }
        else if (horizontalInput > 0)
        {
            animator.SetInteger("Direction", 1); // Moving right
        }
        else
        {
            // Not moving horizontally, keep the direction parameter unchanged
            // You might also set it to 0 or any other value depending on your animation setup
        }

        animator.SetBool("Walking", horizontalInput != 0); // Set walking parameter
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player took " + damage + " damage. Remaining health: " + health);
        // Handle player death or any other logic here
    }

    IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1f); // Wait for 1 second
        canTakeDamage = true;
    }
}
