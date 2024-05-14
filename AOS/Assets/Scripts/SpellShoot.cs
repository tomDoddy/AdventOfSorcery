using UnityEngine;

public class SpellShoot : MonoBehaviour
{
    // Array to store the spells the player has picked up
    private GameObject[] spells = new GameObject[10];

    // Speed of the spell
    public float spellSpeed = 50f;

    // Method to add a spell to the player's spells array
    public void AddSpell(GameObject spellPrefab)
    {
        for (int i = 0; i < spells.Length; i++)
        {
            if (spells[i] == null)
            {
                spells[i] = spellPrefab;
                break;
            }
        }
    }

    // Method to shoot a spell
    public void ShootSpell(GameObject spellPrefab)
    {
        // Instantiate the spell prefab
        GameObject spell = Instantiate(spellPrefab, transform.position, Quaternion.identity);

        // Set the spell's initial velocity to move forward
        Rigidbody2D spellRigidbody = spell.GetComponent<Rigidbody2D>();
        if (spellRigidbody != null)
        {
            spellRigidbody.velocity = transform.right * spellSpeed; // Use transform.right to move in the direction the player is facing
        }

        // Destroy the spell after a certain time if it doesn't hit anything
        Destroy(spell, 0.5f);
    }
}
