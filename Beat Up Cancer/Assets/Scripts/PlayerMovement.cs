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
    private const string JUMP_ANIMATION = "jump";

    // Attack animation triggers
    private const string ATTACK_1 = "attack_1"; 
    private const string ATTACK_2 = "attack_2";
    private const string ATTACK_3 = "attack_3";

    private bool isAttacking = false;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        HandleInput();
        AnimatePlayer();
    }

    private void HandleInput()
    {
        // If attacking, disable movement input
        if (isAttacking) return;

        movementX = Input.GetAxisRaw("Horizontal");
        body.linearVelocity = new Vector2(movementX * speed, body.linearVelocity.y);

        if (movementX < 0)
        {
            sprite.flipX = true;
        }
        else if (movementX > 0)
        {
            sprite.flipX = false;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpingPower);
            anim.SetBool(JUMP_ANIMATION, true);
        }

        if (Input.GetButtonUp("Jump") && body.linearVelocity.y > 0)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y * 0.5f);
        }

        // Attack inputs
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TriggerAttack(ATTACK_1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TriggerAttack(ATTACK_2);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            TriggerAttack(ATTACK_3);
        }
    }

    private void TriggerAttack(string attackTrigger)
    {
        isAttacking = true;
        anim.SetTrigger(attackTrigger);
    }

    // This method is called from animation events at the end of attack animations
    public void EndAttack()
    {
        isAttacking = false;
    }

    private void AnimatePlayer()
    {
        if (isAttacking) return; // Don't play movement animations while attacking

        if (!IsGrounded())
        {
            anim.SetBool(JUMP_ANIMATION, true);
        }
        else
        {
            anim.SetBool(JUMP_ANIMATION, false);

            if (movementX != 0)
            {
                anim.SetBool(WALK_ANIMATION, true);
            }
            else
            {
                anim.SetBool(WALK_ANIMATION, false);
            }
        }
    }

    private bool IsGrounded()
    {
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
            if (transform.position.y > collision.transform.position.y)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
