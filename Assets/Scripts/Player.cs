using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpeed = 5;
    public Transform groundCheck;
    public float jumpForce;

    private Rigidbody2D rb;
    private float speed;
    private bool facingRight = true;
    private bool onGround;
    private bool jump = false;
    private bool doubleJump;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = maxSpeed;
        
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (onGround)
        {
            doubleJump = false;
        }


        if (Input.GetButtonDown("Jump") && (onGround || !doubleJump))
        {
            jump = true;
            if (!doubleJump && !onGround)
            {
                doubleJump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(h * speed, rb.velocity.y);
;

        if ((h > 0 && !facingRight) || (h < 0 && facingRight))
        {
            Flip();
        }

        if (jump)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
            jump = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
