using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witcher : Enemy
{
    public int health = 300;
    public int damage = 50;
    private Transform player;
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private Animator anim;
    private Vector3 playerDistance;
    private bool facingRight = false;
    private bool isDead = false;
    private SpriteRenderer sprite;
    private WitcherAttack attack;
    private bool attackAllowed = true;
    private float lastAttackTime;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        attack = GetComponentInChildren<WitcherAttack>();
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            playerDistance = player.transform.position - transform.position;
            if (attackAllowed && Mathf.Abs(playerDistance.x) < 10 && Mathf.Abs(playerDistance.y) < 3)
            {
                anim.SetTrigger("Attack");
                if (attack != null)
                {
                    WitcherAttack newAttack = Instantiate(attack, attack.transform.position, Quaternion.identity);
                    newAttack.MagicBall((playerDistance.x) / Mathf.Abs(playerDistance.x));
                    attackAllowed = false;
                    lastAttackTime = Time.time;
                }
            }
            if (!attackAllowed && Time.time - lastAttackTime >= 4f)
            {
                attackAllowed = true;
            }
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
            rb.gravityScale = 0;
            box.enabled = false;
            isDead = true;
            rb.velocity = Vector2.zero;
            //Destroy(gameObject);
            anim.SetTrigger("Death");
        }
        else
        {
            StartCoroutine(DamageCoroutine());
        }
    }

    public override IEnumerator DamageCoroutine()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.right * 3 * (-playerDistance.x) / Mathf.Abs(playerDistance.x), ForceMode2D.Impulse);
        for (float i = 0; i<0.2f; i += 0.2f)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.3f);
        }
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
        rb.velocity = Vector2.zero;
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
            yield return new WaitForSeconds(0.8f);
        }
    }
    public override void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
