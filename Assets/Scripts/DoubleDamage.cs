using UnityEngine;

public class DoubleDamage : MonoBehaviour
{
    public float duration = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            KillCounter.ActivateDoublePoints(duration);
            Destroy(gameObject);
        }
    }
}
