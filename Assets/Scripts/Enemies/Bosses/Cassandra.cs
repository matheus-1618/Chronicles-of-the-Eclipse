using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cassandra : Enemy
{
    // Start is called before the first frame update
    public GameObject LevelChanger;
    public int Maxhealth = 3000;
    private int health;
    public int damage = 20;
    public Scrollbar mainSlider;
    public AudioSource attackSound;


    private Transform player;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 playerDistance;
    private bool facingRight = false;
    private bool isDead = false;
    private SpriteRenderer sprite;

    private CassandraAttack1 attack1;
    private CassandraAttack2 attack2;
    private CassandraAttack3 attack3;
    private CassandraAttack5 attack5;
    private bool attackAllowed = true;
    private float lastAttackTime;
    private bool initial = true;
    private int state = 0;
    void Start()
    {
        attackSound.Stop();
        //Soundtrack.Stop();
        //BattleSound.Play();
        health = Maxhealth;
        mainSlider.value = 0;
        mainSlider.size = 1;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        attack1 = GetComponentInChildren<CassandraAttack1>();
        attack2 = GetComponentInChildren<CassandraAttack2>();
        attack3 = GetComponentInChildren<CassandraAttack3>();
        attack5 = GetComponentInChildren<CassandraAttack5>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
        {
            playerDistance = player.transform.position - transform.position;
            if (initial)
            {
                rb.velocity = new Vector2(4.5f * (playerDistance.x) / Mathf.Abs(playerDistance.x), rb.velocity.y);
                anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
                if (Mathf.Abs(playerDistance.x) < 3.2)
                {
                    anim.SetFloat("Speed", 0f);
                    ChangeState();
                    initial = false;
                }

            }
            if (state == 1 && attackAllowed && !initial)
            {
                anim.SetTrigger("Attack1");
                attackSound.Play();
                rb.velocity = new Vector2(2f * (playerDistance.x) / Mathf.Abs(playerDistance.x), rb.velocity.y);
                attack1.Blade();
                attackAllowed = false;
                lastAttackTime = Time.time;
            }

            if (state == 1 && Time.time - lastAttackTime > 2.2f && !initial)
            {
                attackAllowed = true;
                initial = true;
            }

            if (state == 2 && attackAllowed && !initial)
            {
                anim.SetTrigger("Attack2");
                attackSound.Play();
                rb.velocity = new Vector2(1.7f * (playerDistance.x) / Mathf.Abs(playerDistance.x), rb.velocity.y);
                attack2.Blade();
                attackAllowed = false;
                lastAttackTime = Time.time;
            }

            if (state == 2 && Time.time - lastAttackTime > 2.5f && !initial)
            {
                attackAllowed = true;
                initial = true;
            }

            if (state == 3 && attackAllowed && !initial)
            {
                anim.SetTrigger("Attack3");
                attackSound.Play();
                rb.velocity = new Vector2(1.5f * (playerDistance.x) / Mathf.Abs(playerDistance.x), rb.velocity.y);
                attack3.Blade();
                attackAllowed = false;
                lastAttackTime = Time.time;
            }

            if (state == 3 && Time.time - lastAttackTime > 3f && !initial)
            {
                attackAllowed = true;
                initial = true;
            }

            if (state == 4 && attackAllowed && !initial)
            {
                anim.SetTrigger("Attack5");
                attackSound.Play();
                rb.velocity = new Vector2(1.5f * (playerDistance.x) / Mathf.Abs(playerDistance.x), rb.velocity.y);
                attack5.Blade();
                attackAllowed = false;
                lastAttackTime = Time.time;
            }

            if (state == 4 && Time.time - lastAttackTime > 2f && !initial)
            {
                attackAllowed = true;
                initial = true;
            }


            float h = (playerDistance.x) / Mathf.Abs(playerDistance.x);
            if ((h > 0 && !facingRight) || (h < 0 && facingRight))
            {
                Flip();
            }
        }

    }

    void ChangeState()
    {
        if (state == 0)
        {
            state = 1;
        }
        else if (state == 1)
        {
            state = 2;
        }
        else if (state == 2)
        {
            state = 3;
        }
        else if (state == 3)
        {
            state = 4;
        }
        else if (state == 4)
        {
            state = 1;
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
        mainSlider.size = (float)health / Maxhealth;
        if (health <= 0)
        {
            isDead = true;
            rb.velocity = Vector2.zero;
            anim.SetTrigger("Death");
            rb.gravityScale = 0;
            BoxCollider2D box = GetComponent<BoxCollider2D>();
            box.enabled = false;
        }
        else
        {
            StartCoroutine(DamageCoroutine());
        }
    }

    public override IEnumerator DamageCoroutine()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.right * 5 * (-playerDistance.x) / Mathf.Abs(playerDistance.x), ForceMode2D.Impulse);
        //anim.SetTrigger("Damage");
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
        FelixController player = other.gameObject.GetComponent<FelixController>();
        if (player != null)
        {
            StartCoroutine(StopRoutine());
            float directionVector = (player.transform.position.x - transform.position.x) / Mathf.Abs(player.transform.position.x - transform.position.x);
            player.TakeDamage(damage, directionVector);
           
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
        LevelChanger.SetActive(true);
        Destroy(gameObject);
    }
}