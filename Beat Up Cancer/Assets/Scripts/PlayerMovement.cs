using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private bool isAttacking = false;


    [SerializeField] private Transform attackPoint; // Reference to the AttackPoint in the Inspector
    [SerializeField] private float attackRange = 1f; // Adjust the range as needed
    // [SerializeField] private int attackDamage = 10; // How much damage you deal

    public float minX = -10f; // Left boundary
    public float maxX = 10f;  // Right boundary

    public Transform firePoint; // Point where the fireball is spawned
    public GameObject fireballPrefab; // Drag your FireballPrefab here

    private AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip fireBallSound;
    public AudioClip fireArrowSound;

    private float lastSoundTime = 0f;
    public float soundCooldown = 5f; // half a second cooldown

 
    public int currentArrows = 221;
    public int maxArrows = 20;

    public TMP_Text ammoText; 

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        UpdateArrowUI();
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TriggerAttack(ATTACK_1);
            PerformSwordAttack(30);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            TriggerAttack(ATTACK_2);
            PerformSwordAttack(40);

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (currentArrows > 0)
            {
                currentArrows--;
                UpdateArrowUI();
                TriggerAttack(ATTACK_3);
                PerformSwordAttack(50);
            }
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

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (transform.position.y > collision.transform.position.y)
            {
                KillCounter.kills += 1;
                Destroy(collision.gameObject);
            }
        }
    }*/

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
        if (firePoint)
        {
            firePoint.localPosition = new Vector3(Mathf.Abs(firePoint.localPosition.x) * direction, firePoint.localPosition.y, firePoint.localPosition.z);
        }
    }


    void CastFireball()
    {
        Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        if (Time.time - lastSoundTime > soundCooldown)
        {
            audioSource.PlayOneShot(fireBallSound);
            lastSoundTime = Time.time;
        }
    }

    public void PlayAttackSound()
    {
        if (Time.time - lastSoundTime > soundCooldown)
        {
            audioSource.PlayOneShot(attackSound);
            lastSoundTime = Time.time;
        }
    }

    public void PlayArrowSound()
    {
        if (Time.time - lastSoundTime > soundCooldown)
        {
            audioSource.PlayOneShot(fireArrowSound);
            lastSoundTime = Time.time;
        }
    }

    public void ReplenishArrows(int amount)
    {
        int oldArrows = currentArrows;
        currentArrows = Mathf.Min(currentArrows + amount, maxArrows);
        
        if (currentArrows > oldArrows)
        {
            UpdateArrowUI();
        }
    }

    public void UpdateArrowUI()
    {
        GameObject textObj = GameObject.FindWithTag("AmmoText");

        if (textObj != null)
        {
            ammoText = textObj.GetComponent<TMP_Text>();
        }
        else
        {
            Debug.LogWarning("AmmoText object not found with tag 'AmmoText'");
        }


        ammoText.text = "Special Ammo: " + currentArrows;
    }
}
