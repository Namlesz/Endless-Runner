using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isGrounded;
    bool isJumping;
    bool jumpKeyHeld;
    float jumpForce;
    public float speed;
    Vector2 counterJumpForce;
    private Rigidbody2D playerRigidBody;
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        jumpForce = CalculateJumpForce(Physics2D.gravity.magnitude, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpKeyHeld = true;
            if (isGrounded)
            {
                isJumping = true;
                playerRigidBody.AddForce(Vector2.up * jumpForce * playerRigidBody.mass, ForceMode2D.Impulse);
            }
        }
        else if (Input.GetButtonUp("Jump"))
        {
            jumpKeyHeld = false;
        }

        playerRigidBody.AddForce(new Vector2(Input.GetAxis("Horizontal") *speed, 0f));

    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            if (!jumpKeyHeld && Vector2.Dot(playerRigidBody.velocity, Vector2.up) > 0)
            {
                playerRigidBody.AddForce(counterJumpForce * playerRigidBody.mass);
            }
        }
    }

    public static float CalculateJumpForce(float gravityStrength, float jumpHeight)
    {
        return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Floor"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Floor"))
            isGrounded = false;
    }
}
