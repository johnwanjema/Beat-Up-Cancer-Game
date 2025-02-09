using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") ||collision.CompareTag("Player") )
        {
            Destroy(collision.gameObject);
        }
    }
}
