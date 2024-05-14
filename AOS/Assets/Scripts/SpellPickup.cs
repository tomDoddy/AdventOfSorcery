using UnityEngine;

public class SpellPickup : MonoBehaviour
{
    // Reference to the spell prefab to be picked up
    public GameObject spellPrefab;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add the picked up spell to the player's spells array
            other.GetComponent<SpellShoot>().AddSpell(spellPrefab);
            // Destroy the spellbook object after picking it up
            Destroy(gameObject);
        }
    }
}
