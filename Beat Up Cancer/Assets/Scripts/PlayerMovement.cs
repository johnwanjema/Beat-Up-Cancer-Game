using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;
    float speed = 10;
    private float jumpingPower = 5f;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
        // 2D Movement
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            body.linearVelocity = new UnityEngine.Vector2(body.linearVelocity.x, jumpingPower);
        }        
    }
}
