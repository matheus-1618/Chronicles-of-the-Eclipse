using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skull : Enemy
{
    public GameObject collectible;
    public int health = 100;
    public int damage = 50;
    private Transform player;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 playerDistance;
    private bool isDead = false;
    private SpriteRenderer sprite;
    private bool move = true;
    public Vector2 direction = Vector2.right;

    private bool attackAllowed = true;
    private bool leftDirection = true;
    private float lastAttackTime;
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
            if (attackAllowed)
            {
                if (leftDirection)
                {
                    transform.Translate(direction.normalized * -5* Time.deltaTime);
                }
                else
                {
                    transform.Translate(direction.normalized * 5 * Time.deltaTime);
                }
                anim.SetTrigger("Attack");
                StartCoroutine(AttackRoutine());
                lastAttackTime = Time.time;
            }
            if (!attackAllowed && Time.time - lastAttackTime >= 2.5f)
            {
                attackAllowed = true;
                leftDirection = !leftDirection;
                Flip();
            }

          
        }

    }
    public override void Flip()
    {
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
            anim.SetTrigger("Death");
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
        //rb.AddForce(Vector2.right * 5 * (-playerDistance.x) / Mathf.Abs(playerDistance.x), ForceMode2D.Impulse);
        for (float i = 0; i < 0.2f; i += 0.2f)
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
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
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
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
            yield return new WaitForSeconds(0.8f);
        }
        move = true;
    }

    public IEnumerator AttackRoutine()
    {
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
            yield return new WaitForSeconds(2.3f);
        }
        attackAllowed = false;
    }
    public override void DestroyEnemy()
    {
        Instantiate(collectible, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
