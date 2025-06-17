using UnityEngine;

public class AmmoReplenishPickup : MonoBehaviour
{
    public int ammoAmount = 3; // Amount of ammo to replenish

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.LogError("Player object not found! Make sure the player has the 'Player' tag.");

        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.ReplenishArrows(ammoAmount);
                Destroy(gameObject); // Remove pickup after collecting
            }
        }
    }
}
