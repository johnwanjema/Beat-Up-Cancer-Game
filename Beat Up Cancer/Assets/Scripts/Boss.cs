using UnityEngine;

public class Boss : MonoBehaviour
{
    private  float speed = 4;
    [SerializeField] private LayerMask groundLayer;
    private  int health = 1000;
    private int jumpSpeed = 40;
    public float flashCooldown = 0;
    private Rigidbody2D mybody;
    private BoxCollider2D boxCollider;

    private int directionMultiplier = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mybody =  GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (transform.position.y > 0)
        //{
        //    mybody.linearVelocity = new Vector2(mybody.linearVelocity.x, mybody.linearVelocity.y - 0.5f);
        //}
        if (FindPlayer().transform.position.x - transform.position.x < 0 && (IsGrounded() || transform.position.y > 7 || Mathf.Abs(mybody.linearVelocity.x) < 1))
        {
            directionMultiplier = -1;
        }
        else if (IsGrounded() || transform.position.y > 7 || Mathf.Abs(mybody.linearVelocity.x) < 1)
        {
            directionMultiplier = 1;
        }
        if (IsGrounded() && mybody.linearVelocity.y < 0)
        {
            mybody.linearVelocity = new Vector2(mybody.linearVelocity.x, 0);
        }
        // velocity as a force to move player up
        // move enemy on the x- axis 
        if (IsGrounded() || (mybody.linearVelocity.x == 0 && mybody.linearVelocity.y == 0))
        {
            mybody.linearVelocity = new Vector2(mybody.linearVelocity.x, jumpSpeed);
        }
        mybody.linearVelocity = new Vector2(speed * directionMultiplier, mybody.linearVelocity.y);
        if (flashCooldown <= 0)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            flashCooldown -= Time.deltaTime;
        }
    }

    /*
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox") {
            GameManager.instance.ScoreIncrement(100);
            Destroy(gameObject);
        }
    }
    */
    public Collider2D FindPlayer()
    {

        // Detect all colliders within the attack range (without LayerMask)
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 10000);

        // Loop through the detected colliders
        foreach (Collider2D collider in hitColliders)
        {
            // Check if the collider has the "Enemy" tag
            if (collider.CompareTag("Player"))
            {
                // Try to get the Enemy script and apply damage
                return collider;
            }
        }
        return null;
    }
    public void TakeDamage(int damage)
    {
        // Enemy has slight time where can't be hurt
        if (flashCooldown > 0.1) {
            return;
        }
        health -= damage;
        flashCooldown = 0.2f;
        GetComponent<SpriteRenderer>().color = Color.red;
        if (health <= 0){
            KillCounter.kills += 1;
            if (KillCounter.doublePoints)
            {
                KillCounter.boostedKills += 1;
            }
            GameManager.instance.ScoreIncrement(100);
            Destroy(gameObject);
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

}
