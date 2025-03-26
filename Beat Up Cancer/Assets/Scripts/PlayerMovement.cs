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

    private const string RUN_ANIMATION = "run";
    private const string JUMP_ANIMATION = "jump";

    // Attack animation triggers
    private const string ATTACK_1 = "attack_1"; 
    private const string ATTACK_2 = "attack_2";
    private const string ATTACK_3 = "attack_3";
    private const string SHOOT_ARROW = "shoot_arrow";

    private bool isAttacking = false;

    [SerializeField] private GameObject arrowPrefab; // The arrow prefab
    [SerializeField] private float arrowSpeed = 15f; // Speed of the arrow

    [SerializeField] private Transform attackPoint; // Reference to the AttackPoint in the Inspector
    [SerializeField] private float attackRange = 1f; // Adjust the range as needed
    // [SerializeField] private int attackDamage = 10; // How much damage you deal

    public float minX = -10f; // Left boundary
    public float maxX = 10f;  // Right boundary

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        UpdateAttackPoint();
        HandleInput();
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX); // Restrict movement
        transform.position = pos;
        // TODO : fix player animation
        // AnimatePlayer();
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

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpingPower);
            // anim.SetBool(JUMP_ANIMATION, true);
        }

        if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) && body.linearVelocity.y > 0)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y * 0.5f);
        }

        // Attack inputs
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TriggerAttack(ATTACK_1);
            PerformSwordAttack(20);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TriggerAttack(ATTACK_2);
            PerformSwordAttack(25);

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            TriggerAttack(ATTACK_3);
            PerformSwordAttack(50);
        }

        // Handle arrow shooting logic
        if (Input.GetKeyDown(KeyCode.F)) // Press 'F' to shoot
        {
            anim.SetTrigger(SHOOT_ARROW);
        }
    }

    private void ShootArrow()
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
                anim.SetBool(RUN_ANIMATION, true);
            }
            else
            {
                anim.SetBool(RUN_ANIMATION, false);
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

   public void PerformSwordAttack(int attackDamage)
    {

        // Detect all colliders within the attack range (without LayerMask)
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        // Loop through the detected colliders
        foreach (Collider2D collider in hitColliders)
        {
            // Check if the collider has the "Enemy" tag
            if (collider.CompareTag("Enemy"))
            {
                // Try to get the Enemy script and apply damage
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(attackDamage);
                }
                else
                {
                    Debug.LogWarning($"{collider.name} is tagged as 'Enemy' but has no Enemy script.");
                }
            }
        }
    }

   void OnDrawGizmos()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    void UpdateAttackPoint()
    {
        float direction = sprite.flipX ? -1f : 1f;
        attackPoint.localPosition = new Vector3(Mathf.Abs(attackPoint.localPosition.x) * direction, attackPoint.localPosition.y, attackPoint.localPosition.z);
    }

}
