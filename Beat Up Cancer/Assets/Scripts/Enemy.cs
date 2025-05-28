using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public  float speed;
    [SerializeField] private LayerMask groundLayer;
    public  int health = 100;
    public int jumpSpeed = 11;
    public float flashCooldown = 0;
    private Rigidbody2D mybody;
    private BoxCollider2D boxCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mybody =  GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // velocity as a force to move player up
        // move enemy on the x- axis 
        if (IsGrounded() || (mybody.linearVelocity.x == 0 && mybody.linearVelocity.y == 0)) {
            mybody.linearVelocity = new Vector2(mybody.linearVelocity.x, jumpSpeed);
        }
        mybody.linearVelocity = new Vector2(speed,mybody.linearVelocity.y);
        if (flashCooldown <= 0) {
            GetComponent<SpriteRenderer>().color = Color.white;
        } else {
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
