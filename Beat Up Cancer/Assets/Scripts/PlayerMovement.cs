using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;
    float speed = 10;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);
    }
}
