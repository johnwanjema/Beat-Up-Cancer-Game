using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private GameObject arrowPrefab; // The arrow prefab
    [SerializeField] private float arrowSpeed = 15f; // Speed of the arrow

    private const string SHOOT_ARROW = "shoot_arrow"; // Animation trigger

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Handle arrow shooting logic
        if (Input.GetKeyDown(KeyCode.V)) // Press 'F' to shoot
        {
            anim.SetTrigger(SHOOT_ARROW);
        }
    }

    private void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

        // Determine direction based on player facing
        float direction = sprite.flipX ? -1f : 1f;

        // Set the velocity of the arrow
        rb.linearVelocity = new Vector2(direction * arrowSpeed, 0f);

        // Optional: Flip the arrow sprite if shooting left
        SpriteRenderer arrowSprite = arrow.GetComponent<SpriteRenderer>();
        if (direction < 0)
        {
            arrowSprite.flipX = true;
        }
    }
}
