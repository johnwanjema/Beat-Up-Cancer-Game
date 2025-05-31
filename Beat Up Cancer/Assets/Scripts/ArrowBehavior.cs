using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Destroy the enemy object
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(150);
            } 
            else
            {
                Boss boss = collision.GetComponent<Boss>();
                if (boss != null)
                {
                    boss.TakeDamage(150);
                }
                else
                {
                    Debug.LogWarning($"{collision.name} is tagged as 'Enemy' but has no Enemy script.");
                }
            }
            Destroy(gameObject);
        }
    }
}
