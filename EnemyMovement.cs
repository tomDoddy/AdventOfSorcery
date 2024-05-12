using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float raycastDistance = 1f;
    public float fovAngle = 30f;
    public float attackRange = 2f;

    private bool movingRight = true;
    private Transform player;
    private bool playerInSight = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Move();
        CheckForPlayer();
    }

    private void Move()
    {
        Vector2 movement = Vector2.right * (movingRight ? 1 : -1);
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement, raycastDistance);
        if (hit.collider != null && !hit.collider.CompareTag("Ground"))
        {
            movingRight = !movingRight;
            Flip();
        }
    }

    private void CheckForPlayer()
    {
        Vector2 directionToPlayer = player.position - transform.position;
        float angle = Vector2.Angle(directionToPlayer, transform.right);

        if (angle <= fovAngle / 2f)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer.normalized, Mathf.Infinity);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                playerInSight = true;
                Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, player.position) <= attackRange)
                {
                    AttackPlayer();
                }
            }
            else
            {
                playerInSight = false;
            }
        }
        else
        {
            playerInSight = false;
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void AttackPlayer()
    {
        // Add attack logic here
        Debug.Log("Attacking Player!");
    }
}
