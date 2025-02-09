using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    Rigidbody2D body;
    SpriteRenderer sprite;
    BoxCollider2D boxCollider;
    float speed = 5;
    private float jumpingPower = 9;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {   
        // 2D Movement
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);

        if (body.linearVelocity.x < 0) {
            sprite.flipX = true;
        } else if (body.linearVelocity.x > 0) {
            sprite.flipX = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, 2 * jumpingPower);
        }  
        if (!Input.GetButton("Jump") && body.linearVelocity.y > 0) {
            body.linearVelocity = new Vector2(body.linearVelocity.x, 0);
        }      
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Check if the player landed on the enemy from above
            if (transform.position.y > collision.transform.position.y)
            {
                Destroy(collision.gameObject);
                // collision.gameObject.GetComponent<Enemy>().Die();
            }
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
