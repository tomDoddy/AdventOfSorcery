using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 5f;
    public Rigidbody2D rb;

    private bool canJump = true;

    private void Start()
    {
        GameObject[] enemyColliders = GameObject.FindGameObjectsWithTag("enemycol"); //baso it will ignore things tagged 'enemycol'

        foreach (GameObject enemyCollider in enemyColliders)
        {
            Collider2D enemyCol = enemyCollider.GetComponent<Collider2D>(); //baso says any and all 2D colliders used with the tag are ignored by the player
            if (enemyCol != null)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemyCol, true); //just says to ignore collision lol
            }
        }
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y); //movement init
        rb.velocity = movement;

        if (Input.GetKeyDown(KeyCode.Space) && canJump) //baso u click space and it jumps and checks if u can jump
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); //the uh force ur guy can jump 
        canJump = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) //baso lets u jump if its touching somn tagged 'Ground'
        {
            canJump = true;
        }
    }
}
