using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class AriaController : PlayerController
{
    // Start is called before the first frame update
    public AudioSource jumpSound;
    public AudioSource soudtrack;
    public AudioSource damageSound;
    public AudioSource soundAttack1;
    public AudioSource soundAttack2;
    public AudioSource soundAttack3;
    public static float dodgeTime = 2f;
    public static int attackImprovement = 0;
    public static int Maxhealth = 500;
    public static int rings = 0;
    public static int cures = 0;
    public TextMeshProUGUI CureCount;
    public TextMeshProUGUI RingCount;
    public TextMeshProUGUI RingCountPause;
    public float maxSpeed = 5;
    public Transform groundCheck;
    public float jumpForce;
    public Scrollbar mainSlider;
    public Image Imageattack1;
    public Image Imageattack10;
    public Image Imageattack2;
    public Image Imageattack20;
    public Image Imageattack3;
    public Image Imageattack30;
    public Image Imageattack4;
    public Image Imageattack40;
    public Image Dodge;
    public Image Dodge0;
    private bool canDamage = true;
    private bool doubleJump;
    private int health;

    private Rigidbody2D rb;
    private float speed;
    private bool facingRight = true;
    private bool onGround;
    private bool jump = false;
    private Animator anim;
    private SpriteRenderer sprite;

    private bool roll = true;
    private bool canRoll = false;
    private float lastRollTime;

    //Atack1
    private bool canAttack1 = true;
    private float lastAttack1Time;
    private AriaAttack1 attack1;

    //Attack2
    private bool canAttack2 = true;
    private float lastAttack2Time;
    private AriaAttack2 attack2;
    private bool stop = false;
    private int direction = 1;


    //Atack1
    private bool canAttack3 = true;
    private float lastAttack3Time;
    private AriaAttack3 attack3;


    //Atack1
    private bool canAttack4 = true;
    private float lastAttack4Time;
    private AriaAttack4 attack4;

    //Atack1
    private bool canAttack5 = true;
    private float lastAttack5Time;
    private AriaAttack5 attack5;

    void Start()
    {
        soudtrack.Play();
        damageSound.Stop();
        soundAttack1.Stop();
        soundAttack2.Stop();
        soundAttack3.Stop();
        jumpSound.Stop();
        CureCount.text = cures.ToString() + "x";
        RingCount.text = rings.ToString();
        RingCountPause.text = rings.ToString();
        mainSlider.value = 0;
        mainSlider.size = 1;
        rb = GetComponent<Rigidbody2D>();
        health = Maxhealth;
        speed = maxSpeed;
        anim = GetComponent<Animator>();
        attack1 = GetComponentInChildren<AriaAttack1>();
        attack2 = GetComponentInChildren<AriaAttack2>();
        attack3 = GetComponentInChildren<AriaAttack3>();
        attack4 = GetComponentInChildren<AriaAttack4>();
        attack5 = GetComponentInChildren<AriaAttack5>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (canAttack1 && Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("Attack1");
            soundAttack3.Play();
            attack1.Blade(attackImprovement);
            canAttack1 = false;
            lastAttack1Time = Time.time;
            stop = true;
            Imageattack1.color = Color.red;
            Imageattack10.color = Color.red;
        }
        if (!canAttack1 && Time.time - lastAttack1Time >= 0.6f)
        {
            canAttack1 = true;
            stop = false;
            Imageattack1.color = Color.white;
            Imageattack10.color = Color.white;
        }

        if (canAttack2 && Input.GetKeyDown(KeyCode.X))
        {
            stop = true;
            anim.SetTrigger("Attack2");
            soundAttack2.Play();
            AriaAttack2 newAttack2 = Instantiate(attack2, attack2.transform.position, Quaternion.identity);
            newAttack2.MagicBall(direction, attackImprovement);
            canAttack2 = false;
            lastAttack2Time = Time.time;
            Imageattack2.color = Color.red;
            Imageattack20.color = Color.red;
        }

        if (!canAttack2 && Time.time - lastAttack2Time >= 0.8f)
        {
            canAttack2 = true;
            stop = false;
            Imageattack2.color = Color.white;
            Imageattack20.color = Color.white;
        }

        if (canAttack3 && Input.GetKeyDown(KeyCode.C))
        {
            anim.SetTrigger("Attack5");
            soundAttack1.Play();
            attack5.Blade(attackImprovement);
            canAttack3 = false;
            lastAttack3Time = Time.time;
            stop = true;
            Imageattack3.color = Color.red;
            Imageattack30.color = Color.red;
        }
        if (!canAttack3 && Time.time - lastAttack3Time >= 1.5f)
        {
            canAttack3 = true;
            stop = false;
            Imageattack3.color = Color.white;
            Imageattack30.color = Color.white;
        }

        if (canAttack4 && Input.GetKeyDown(KeyCode.V))
        {
            anim.SetTrigger("Attack4");
            soundAttack2.Play();
            attack4.Blade(attackImprovement);
            canAttack4 = false;
            lastAttack4Time = Time.time;
            stop = true;
            Imageattack4.color = Color.red;
            Imageattack40.color = Color.red;
        }
        if (!canAttack4 && Time.time - lastAttack4Time >= 0.9f)
        {
            canAttack4 = true;
            stop = false;
            Imageattack4.color = Color.white;
            Imageattack40.color = Color.white;
        }

        if (onGround && roll && Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("Roll");
            roll = false;
            canRoll = true;
            lastRollTime = Time.time;
            Dodge.color = Color.red;
            Dodge0.color = Color.red;
        }
        if (!roll && Time.time-lastRollTime > dodgeTime)
        {
            roll = true;
            Dodge.color = Color.white;
            Dodge0.color = Color.white;
        }

        if (cures > 0 && Input.GetKeyDown(KeyCode.E))
        {
            cures -= 1;
            CureCount.text = cures.ToString() + "x";
            anim.SetTrigger("Potion");
            health += 200;
            mainSlider.size = (float)health / Maxhealth;
            StartCoroutine(CureCoroutine());
        }

        if (transform.position.y < -5)
        {
            int cenaAtual = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(cenaAtual);
        }

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

        if (canRoll)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            jumpSound.Play();
            Vector2 Ndirection = Vector2.right;
            transform.Translate(Ndirection.normalized * 45 * direction * Time.deltaTime);
            //rb.AddForce(Vector2.right * -10 * direction, ForceMode2D.Impulse);
            canRoll = false;
        }

        if (jump)
        {
            anim.SetTrigger("Jump");
            jumpSound.Play();
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

    public override void GetRing()
    {
        rings += 1;
        RingCount.text = rings.ToString();
        RingCountPause.text = rings.ToString();
    }

    public override void GetCure()
    {
        cures += 1;
        CureCount.text = cures.ToString() + "x";
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
            mainSlider.size = (float) health/Maxhealth;
            if (health <= 0)
            {
                anim.SetTrigger("Dead");
                StartCoroutine(RecarregaCena());
            }
            else
            {
                //rb.AddForce(Vector2.right * 5 * direction, ForceMode2D.Impulse);
                //anim.SetTrigger("Damage");
                StartCoroutine(DamageCoroutine());

            }
        }
    }

    public override void SetMaxHealth(int healthExtra) {
        health += 100;
        Maxhealth += healthExtra;
        Vector3 scale = mainSlider.transform.localScale;
        scale.x *= 1.1f;
        mainSlider.transform.localScale = scale;
    }
    public override void SetattackImprovement(int extra)
    {
        attackImprovement += extra;
    }
    public override void SetDodgeTime(float minus)
    {
        if (dodgeTime > 0.6f)
        {
            dodgeTime -= minus;
        }
    }

    
    public override void SetRings(int minus) {
        rings -= minus;
        RingCount.text = rings.ToString();
        RingCountPause.text = rings.ToString();
    }
    public override int GetRingCount() {
        return rings;
    }

    public IEnumerator RecarregaCena()
    {
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
            yield return new WaitForSeconds(0.7f);
            int cenaAtual = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(cenaAtual);
        }
    }
    public override IEnumerator DamageCoroutine()
    {
        damageSound.Play();
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            sprite.color = Color.white;
            // yield return new WaitForSeconds(0.3f);
        }
        canDamage = true;
    }
    public IEnumerator CureCoroutine()
    {
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
            sprite.color = Color.green;
            yield return new WaitForSeconds(0.25f);
            sprite.color = Color.white;
            // yield return new WaitForSeconds(0.3f);
        }
    }
}
