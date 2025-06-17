using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {   

        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            // gameManager?.IncreaseScore();  
        }
        else if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Arrow"))
        {
            Destroy(collision.gameObject);
        }


    }
}
