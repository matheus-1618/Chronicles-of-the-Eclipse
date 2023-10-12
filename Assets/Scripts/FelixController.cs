using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class FelixController : PlayerController
{
    // Start is called before the first frame update
    public float maxSpeed = 5;
    public Transform groundCheck;
    public float jumpForce;
    public int health = 500;
    private bool canDamage = true;

    private Rigidbody2D rb;
    private float speed;
    private bool facingRight = true;
    private bool onGround;
    private bool jump = false;
    private Animator anim;
    private SpriteRenderer sprite;

    //Atack1
    private bool canAttack1 = true;
    private float lastAttack1Time;
    private Attack1 attack1;

    //Attack2
    private bool canAttack2 = true;
    private float lastAttack2Time;
    private Attack2 attack2;
    private bool stop = false;
    private int direction = 1;


    //Atack1
    private bool canAttack3 = true;
    private float lastAttack3Time;
    private Attack3 attack3;


    //Atack1
    private bool canAttack4 = true;
    private float lastAttack4Time;
    private Attack4 attack4;

    //Atack1
    private bool canAttack5 = true;
    private float lastAttack5Time;
    private Attack5 attack5;

    //Atack1
    private bool canAttack6 = true;
    private float lastAttack6Time;
    private Attack6 attack6;


    //Atack1
    private bool canAttack7 = true;
    private float lastAttack7Time;
    private Attack7 attack7;


    //Atack1
    private bool canAttack8 = true;
    private float lastAttack8Time;
    private Attack8 attack8;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = maxSpeed;
        anim = GetComponent<Animator>();
        attack1 = GetComponentInChildren<Attack1>();
        attack2 = GetComponentInChildren<Attack2>();
        attack3 = GetComponentInChildren<Attack3>();
        attack4 = GetComponentInChildren<Attack4>();
        attack5 = GetComponentInChildren<Attack5>();
        attack6 = GetComponentInChildren<Attack6>();
        attack7 = GetComponentInChildren<Attack7>();
        attack8 = GetComponentInChildren<Attack8>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (canAttack1 && Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("attack5");
            attack5.Explosion();
            canAttack1 = false;
            lastAttack1Time = Time.time;
            stop = true;
        }
        if (!canAttack1 && Time.time - lastAttack1Time >= 0.6f)
        {
            canAttack1 = true;
            stop = false;
        }

        if (canAttack2 && Input.GetKeyDown(KeyCode.X))
        {
            stop = true;
            anim.SetTrigger("attack2");
            Attack2 newAttack2 = Instantiate(attack2, attack2.transform.position, Quaternion.identity);
            newAttack2.MagicBall(direction);
            canAttack2 = false;
            lastAttack2Time = Time.time;
        }

        if (!canAttack2 && Time.time - lastAttack2Time >= 1f)
        {
            canAttack2 = true;
            stop = false;
        }

        if (canAttack3 && Input.GetKeyDown(KeyCode.C))
        {
            anim.SetTrigger("attack3");
            attack3.Explosion();
            canAttack3 = false;
            lastAttack3Time = Time.time;
            stop = true;
        }
        if (!canAttack3 && Time.time - lastAttack3Time >= 0.6f)
        {
            canAttack3 = true;
            stop = false;
        }

        if (canAttack4 && Input.GetKeyDown(KeyCode.V))
        {
            anim.SetTrigger("attack6");
            attack6.Explosion();
            canAttack4 = false;
            lastAttack4Time = Time.time;
            stop = true;
        }
        if (!canAttack4 && Time.time - lastAttack4Time >= 0.9f)
        {
            canAttack4 = true;
            stop = false;
        }



        if (Input.GetButtonDown("Jump") && (onGround))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (canDamage)
        {
            rb.velocity = new Vector2(h * speed, rb.velocity.y);
        }

        anim.SetFloat("Speed", Mathf.Abs(h));
        if ((h > 0 && !facingRight) || (h < 0 && facingRight))
        {
            Flip();
        }

        if (stop)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (jump)
        {
            anim.SetTrigger("Jump");
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
            jump = false;
        }
    }

    public override void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        direction *= -1;
    }

    public int GetHealth()
    {
        return health;
    }

    public override void TakeDamage(int damage)
    {
        if (canDamage)
        {
            canDamage = false;
            health -= damage;
            if (health <= 0)
            {
                anim.SetTrigger("Dead");
            }
            else
            {
                anim.SetTrigger("Damage");
                StartCoroutine(DamageCoroutine());

            }
        }
    }
    public override IEnumerator DamageCoroutine()
    {
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
           sprite.color = Color.red;
            yield return new WaitForSeconds(0.3f);
           sprite.color = Color.white;
           // yield return new WaitForSeconds(0.3f);
        }
        canDamage = true;
    }
}
