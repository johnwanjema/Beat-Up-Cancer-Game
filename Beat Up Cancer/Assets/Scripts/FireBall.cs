using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 25;
    public float destroyTime = 3f; // Destroy after 3 seconds

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Get the player by tag and check if it's found
        GameObject player = GameObject.FindGameObjectWithTag("Player");

       if (player != null)
        {
            SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
            bool isFacingLeft = playerSprite != null && playerSprite.flipX;

            float direction = isFacingLeft ? -1f : 1f; // Move left if flipped
            rb.linearVelocity = new Vector2(direction * speed, 0);
        }
        else
        {
            Debug.LogError("Player object not found! Make sure the player has the 'Player' tag.");
        }

        Destroy(gameObject, destroyTime); // Auto-destroy after the specified time
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(50);
            }
            Destroy(gameObject); // Destroy fireball on impact
        }
    }
}
