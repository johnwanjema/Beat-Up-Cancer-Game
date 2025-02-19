using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private BoxCollider2D boxCollider;
    private Animator anim;

    [SerializeField]
    private float speed = 5f;
    private float jumpingPower = 9f;
    private float movementX;

    private const string WALK_ANIMATION = "run";

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");

        // Apply velocity-based movement
        body.linearVelocity = new Vector2(movementX * speed, body.linearVelocity.y);

        // Flip sprite based on velocity
        if (movementX < 0)
        {
            sprite.flipX = true;
        }
        else if (movementX > 0)
        {
            sprite.flipX = false;
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpingPower);
        }

        // Optional: Cut jump height if key is released early
        if (Input.GetButtonUp("Jump") && body.linearVelocity.y > 0)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y * 0.5f); // Smooth cut
        }

        // Animate player movement
        AnimatePlayer();
    }


    private void AnimatePlayer()
    {
        // Handle running animation and sprite flip
        if (movementX > 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sprite.flipX = false;
        }
        else if (movementX < 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sprite.flipX = true;
        }
        else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }
    }

    private bool IsGrounded()
    {
        // Raycast to check if player is on the ground
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.down,
            0.1f,
            groundLayer
        );

        return raycastHit.collider != null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Check if player landed on enemy
            if (transform.position.y > collision.transform.position.y)
            {
                Destroy(collision.gameObject);
                // collision.gameObject.GetComponent<Enemy>().Die(); // Optional
            }
        }
    }
}
