using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beast : Enemy
{
    public float speed = 3;
    public int health = 300;
    public int damage = 50;
    private Transform player;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 playerDistance;
    private bool facingRight = false;
    private bool isDead = false;
    private SpriteRenderer sprite;
    private bool move = true;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isDead && move)
        {
            playerDistance = player.transform.position - transform.position;
            float h = (playerDistance.x) / Mathf.Abs(playerDistance.x);
            if ((h > 0 && !facingRight) || (h < 0 && facingRight))
            {
                Flip();
            }
        }

    }
    public override void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            rb.velocity = Vector2.zero;
            anim.SetTrigger("Dead");
        }
        else
        {
            StartCoroutine(DamageCoroutine());
        }
    }

    public override IEnumerator DamageCoroutine()
    {
        move = false;
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.right * 3 * (-playerDistance.x) / Mathf.Abs(playerDistance.x), ForceMode2D.Impulse);
        for (float i = 0; i<0.2f; i += 0.2f)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.3f);
        }
        move = true;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        FelixController player = other.gameObject.GetComponent<FelixController>();
        if (player != null)
        {
            StartCoroutine(StopRoutine());
            player.TakeDamage(damage);
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5 * (playerDistance.x) / Mathf.Abs(playerDistance.x), ForceMode2D.Impulse);
        }  
    }

    public override IEnumerator StopRoutine()
    {
        move = false;
        rb.velocity = Vector2.zero;
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
            yield return new WaitForSeconds(0.8f);
        }
        move = true;
    }
    public override void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
