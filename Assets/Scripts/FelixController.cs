using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FelixController : MonoBehaviour
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
    private Animator anim;

    //Atack1
    private bool canAttack1 = true;
    private float lastAttack1Time;

    //Attack2
    private bool canAttack2 = true;
    private float lastAttack2Time;
    private Attack2 attack2;
    private int direction = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = maxSpeed;
        anim = GetComponent<Animator>();
        attack2 = GetComponentInChildren<Attack2>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (canAttack1 && Input.GetKeyDown(KeyCode.Z))
        {
            rb.velocity = Vector2.zero;
            anim.SetTrigger("Attack1");
            canAttack1 = false;
            lastAttack1Time = Time.time;
        }
        if (!canAttack1 && Time.time - lastAttack1Time >= 0.6f)
        {
            canAttack1 = true;
        }

        if (canAttack2 && Input.GetKeyDown(KeyCode.X))
        {
            rb.velocity = Vector2.zero;
            anim.SetTrigger("Attack2");
            attack2.MagicBall(direction);
            canAttack2 = false;
            lastAttack2Time = Time.time;
        }

        if (!canAttack2 && Time.time - lastAttack2Time >= 2.5f)
        {
            canAttack2 = true;
        }

    

        if (Input.GetButtonDown("Jump") && (onGround))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(h * speed, rb.velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(h));
        if ((h > 0 && !facingRight) || (h < 0 && facingRight))
        {
            Flip();
        }

        if (jump)
        {
            anim.SetTrigger("Jump");
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
        direction *= -1;
    }
}
