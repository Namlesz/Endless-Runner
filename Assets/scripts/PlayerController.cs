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
                playerRigidBody.AddForce(Vector2.up * jumpForce * playerRigidBody.mass* Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
        }
        else if (Input.GetButtonUp("Jump"))
        {
            jumpKeyHeld = false;
        }
        if (isGrounded && Input.GetAxis("Horizontal") == 0)
        {
            playerRigidBody.velocity = Vector2.zero;
        }
        else
        {
            Vector2 move = new Vector2(Input.GetAxis("Horizontal") * speed, 0f);
            move = move.normalized * Time.fixedDeltaTime * speed;
            playerRigidBody.AddForce(move);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
        else
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02F;
        }

        if (isJumping)
        {
            if (!jumpKeyHeld && Vector2.Dot(playerRigidBody.velocity, Vector2.up) > 0)
            {
                playerRigidBody.AddForce(counterJumpForce * playerRigidBody.mass* Time.fixedDeltaTime);
            }
        }
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
