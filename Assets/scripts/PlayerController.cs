using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isGrounded;
    bool isJumping;
    bool jumpKeyHeld;
    public float jumpForce;
    public float speed;
    Vector2 counterJumpForce;
    private Rigidbody2D playerRigidBody;
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
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
        Vector2 move = new Vector2(Input.GetAxis("Horizontal") * speed, 0f);
        move = move.normalized * Time.deltaTime * speed;
        playerRigidBody.AddForce(move);

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
