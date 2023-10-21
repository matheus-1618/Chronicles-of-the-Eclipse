using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Necromancer : Enemy
{
    // Start is called before the first frame update
    public GameObject LevelChanger;
    public int Maxhealth = 3000;
    private int health;
    public int damage = 20;
    public Scrollbar mainSlider;
    private Transform player;
    public AudioSource attackSound;
    public AudioSource attackSound2;

    public AudioSource Soundtrack;
    public AudioSource BattleSound;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 playerDistance;
    private bool facingRight = false;
    private bool isDead = false;
    private SpriteRenderer sprite;

    private NecromancerAttack1 attack1;
    private NecromancerAttack2 attack2;
    private bool attackAllowed = true;
    private float lastAttackTime;
    private int state = 0;
    void Start()
    {
        attackSound.Stop();
        attackSound2.Stop();
        //Soundtrack.Stop();
        //BattleSound.Play();
        health = Maxhealth;
        mainSlider.value = 0;
        mainSlider.size = 1;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        attack1 = GetComponentInChildren<NecromancerAttack1>();
        attack2 = GetComponentInChildren<NecromancerAttack2>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
        {
            playerDistance = player.transform.position - transform.position;
            if (state == 0 && attackAllowed)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
                anim.SetTrigger("Attack1");
                StartCoroutine(Attack1Routine());
                lastAttackTime = Time.time;
                attackAllowed = false;
            }
            if (state == 0 && Time.time - lastAttackTime > 3f)
            {
                attackAllowed = true;
                state = 1;

            }
            if (state == 1 && attackAllowed)
            {
                rb.velocity = new Vector2(4.5f * (playerDistance.x) / Mathf.Abs(playerDistance.x), rb.velocity.y);
                anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
                if (Mathf.Abs(playerDistance.x) < 2.6f)
                {
                    //rb.velocity = new Vector2(0f, rb.velocity.y);
                    anim.SetFloat("Speed", 0f);
                    state = 2;
                }
            }

            if (state == 2 && attackAllowed)
            {
                anim.SetTrigger("Attack2");
                attackSound2.Play();
                rb.velocity = new Vector2(1.5f * (playerDistance.x) / Mathf.Abs(playerDistance.x), rb.velocity.y);
                if (attack2 != null)
                {
                    NecromancerAttack2 newAttack = Instantiate(attack2, attack2.transform.position, Quaternion.identity);
                    newAttack.Blade((playerDistance.x) / Mathf.Abs(playerDistance.x));
                }
                attackAllowed = false;
                lastAttackTime = Time.time;
            }

            if (state == 2 && Time.time - lastAttackTime > 4f)
            {
                attackAllowed = true;
                state = 0;
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

    public  IEnumerator Attack1Routine()
    {
        anim.SetFloat("Speed", 0f);
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
            float directionFactor = -transform.localScale.x / Mathf.Abs(transform.localScale.x);
            attack1.Blade(0f* directionFactor);
            attackSound.Play();
            yield return new WaitForSeconds(0.8f);
            attack1.Blade(2.5f* directionFactor);
            attackSound.Play();
            yield return new WaitForSeconds(0.8f);
            attack1.Blade(4f* directionFactor);
            attackSound.Play();
            yield return new WaitForSeconds(0.8f);
            attack1.Blade(-6.5f* directionFactor);
            attackSound.Play();
        }
    }


    public override void DestroyEnemy()
    {
        LevelChanger.SetActive(true);
        Destroy(gameObject);
    }
}