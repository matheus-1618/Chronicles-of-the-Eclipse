using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareCrow : Enemy
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    public Vector2 direction = Vector2.right;
    private bool facingRight = false;
    private Vector3 playerDistance;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = player.transform.position - transform.position;
        if ((playerDistance.x > 0 && !facingRight) || (playerDistance.x < 0 && facingRight))
        {
            Flip();
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
        anim.SetTrigger("attack");
        StartCoroutine(DamageCoroutine());
    }

    public override IEnumerator DamageCoroutine()
    {
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            StartCoroutine(StopRoutine());
            float directionVector = (player.transform.position.x - transform.position.x) / Mathf.Abs(player.transform.position.x - transform.position.x);
            player.TakeDamage(10, directionVector);
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
