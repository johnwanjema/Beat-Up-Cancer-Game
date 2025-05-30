using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Destroy the enemy object
            KillCounter.kills += 1;
            if (KillCounter.doublePoints)
            {
                KillCounter.boostedKills += 1;
            }
            Destroy(collision.gameObject);

            // Destroy the arrow after hitting the enemy
            Destroy(gameObject);
        }
    }
}
