using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator anim;
    private PlayerMovement playerMovement;

    [SerializeField] private GameObject arrowPrefab; // The arrow prefab
    [SerializeField] private float arrowSpeed = 15f; // Speed of the arrow

    private const string SHOOT_ARROW = "shoot_arrow"; // Animation trigger

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (playerMovement.currentArrows > 0)
            {
                anim.SetTrigger(SHOOT_ARROW); // Only trigger animation
            }
        }
    }

    // This method should be called via animation event
    private void Shoot()
    {
        if (playerMovement.currentArrows > 0)
        {
            playerMovement.currentArrows--;
            playerMovement.UpdateArrowUI();

            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

            float direction = sprite.flipX ? -1f : 1f;
            rb.linearVelocity = new Vector2(direction * arrowSpeed, 0f);

            SpriteRenderer arrowSprite = arrow.GetComponent<SpriteRenderer>();
            if (direction < 0)
            {
                arrowSprite.flipX = true;
            }
        }
    }
}
