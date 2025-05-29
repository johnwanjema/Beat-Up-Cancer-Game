using UnityEngine;

public class DoubleDamage : MonoBehaviour
{
    public float duration = 10f; // Duration of 2x damage effect

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // TOD0 : activate 2x damage
            Destroy(gameObject); 
        }
    }
}
