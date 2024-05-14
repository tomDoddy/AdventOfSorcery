using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3f;            // Speed of the player's movement
    public float jumpForce = 5f;            // Force applied when jumping
    public Rigidbody2D rb;                  // Reference to the player's Rigidbody2D component

    private bool canJump = true;            // Flag to determine if the player can jump

    // Spells
    public GameObject[] spells;             // Array of spell prefabs

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
    }

    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

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
    }

    void CastSpell(int spellIndex)
    {
        // Instantiate the selected spell at the player's position
        if (spellIndex >= 0 && spellIndex < spells.Length)
        {
            Instantiate(spells[spellIndex], transform.position, Quaternion.identity);
            Debug.Log("Spell " + (spellIndex + 1) + " selected."); // Debug log to indicate selected spell
        }
        else
        {
            Debug.LogWarning("Spell index out of range: " + spellIndex);
        }
    }
}