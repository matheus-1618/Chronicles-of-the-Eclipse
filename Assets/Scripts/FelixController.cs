using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FelixController : PlayerController
{
    // Start is called before the first frame update
    public GameObject buttonCure;
    public GameObject aura;
    public GameObject button1;
    public GameObject button2;
    public DynamicJoystick joystick;
    public GameObject upgradeD;
    public GameObject DeathScene;
    public GameObject BlackFade;
    public GameObject ScenarioName;
    public AudioSource jumpSound;
    public AudioSource soudtrack;
    public AudioSource damageSound;
    public AudioSource soundAttack1;
    public AudioSource soundAttack2;
    public static float healthSize = 1f;
    public static float dodgeTime = 2f;
    public static int attackImprovement = 0;
    public static int Maxmana = 500;
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
    public Scrollbar ManaSlider;
    private bool canDamage = true;
    private int health;
    private int mana;

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

    private bool canAttack = true;
    private float lastAttack;
    void Start()
    {
        Vector3 scale = mainSlider.transform.localScale;
        scale.x = healthSize;
        mainSlider.transform.localScale = scale;
        StartCoroutine(ScenarioRoutine());
        soudtrack.Play();
        damageSound.Stop();
        soundAttack1.Stop();
        soundAttack2.Stop();
        jumpSound.Stop();
        CureCount.text = cures.ToString();
        RingCount.text = rings.ToString();
        RingCountPause.text = rings.ToString();
        rb = GetComponent<Rigidbody2D>();
        mainSlider.value = 0;
        mainSlider.size = 1;
        ManaSlider.value = 0;
        ManaSlider.size = 0;
        health = Maxhealth;
        mana = 0;
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
        Renderer auraRenderer = aura.GetComponent<Renderer>();
        if (auraRenderer != null)
        {
         
            Material auraMaterial = auraRenderer.material;

            Color color = auraMaterial.color;
            color.a = (float) mana / Maxmana;
            auraMaterial.color = color;
        }

        if (cures > 0)
        {
            buttonCure.SetActive(true);
        }
        else
        {
            buttonCure.SetActive(false);
        }

        if (mana >= 500)
        {
            button2.SetActive(true);
        }
        else
        {
            button2.SetActive(false);
        }
        if (mana>= 150)
        {
            button1.SetActive(true);
        }
        else
        {
            button1.SetActive(false);
        }

        if (rings >= 5)
        {
            upgradeD.SetActive(true);
        }
        else
        {
            upgradeD.SetActive(false);
        }
        if (!canAttack && Time.time - lastAttack >= 0.5f)
        {
            canAttack = true;
        }

        if (!canAttack1 && Time.time - lastAttack1Time >= 0.5f)
        {
            canAttack1 = true;
            stop = false;
        }

        if (!canAttack2 && Time.time - lastAttack2Time >= 1f)
        {
            canAttack2 = true;
            stop = false;
        }

        if (!canAttack3 && Time.time - lastAttack3Time >= 1f)
        {
            stop = false;
        }

        if (!canAttack3 && Time.time - lastAttack3Time >= 2f)
        {
            canAttack3 = true;
        }

        if (!canAttack4 && Time.time - lastAttack4Time >= 0.7f)
        {
            canAttack4 = true;
            stop = false;
        }

        if (!roll && Time.time - lastRollTime > dodgeTime)
        {
            roll = true;

        }
        if (transform.position.y < -5)
        {
            anim.SetTrigger("Dead");
            StartCoroutine(RecarregaCena());
        }
    }

    public void RollFunction()
    {
        if (onGround && roll)
        {
            anim.SetTrigger("Roll");
            roll = false;
            canRoll = true;
            lastRollTime = Time.time;
        }
    }
    public void JumpFunction()
    {
        if ((onGround))
        {
            jump = true;
        }
    }
    public void Attack1Function()
    {
        if (canAttack && canAttack1)
        {
            canAttack = false;
            lastAttack = Time.time;
            anim.SetTrigger("attack5");
            soundAttack1.Play();
            attack5.Explosion(attackImprovement);
            canAttack1 = false;
            lastAttack1Time = Time.time;
            stop = true;
        }
    }
    public void Attack2Function()
    {
        if (canAttack && canAttack4)
        {
            canAttack = false;
            lastAttack = Time.time;
            anim.SetTrigger("attack6");
            soundAttack1.Play();
            attack6.Explosion(attackImprovement);
            canAttack4 = false;
            lastAttack4Time = Time.time;
            stop = true;
        }
    }
    public void Attack3Function()
    {
        if (canAttack && canAttack3)
        {
            mana -= 500;
            ManaSlider.size = (float)mana / Maxmana;
            canAttack = false;
            lastAttack = Time.time;
            anim.SetTrigger("attack8");
            attack8.Explosion(attackImprovement);
            
            soundAttack2.Play();
            canAttack3 = false;
            lastAttack3Time = Time.time;
            stop = true;
        }
    }
    public void Attack4Function()
    {
        if (canAttack && canAttack2)
        {
            mana -= 150;
            ManaSlider.size = (float)mana / Maxmana;
            canAttack = false;
            lastAttack = Time.time;
            stop = true;
            anim.SetTrigger("attack4");
            attack4.Explosion(attackImprovement);
            //anim.SetTrigger("attack2");
            soundAttack2.Play();
            //Attack2 newAttack2 = Instantiate(attack2, attack2.transform.position, Quaternion.identity);
            //newAttack2.MagicBall(direction, attackImprovement);
            canAttack2 = false;
            lastAttack2Time = Time.time;
        }
    }

    public void CureFunction()
    {
        if (cures > 0)
        {
            cures -= 1;
            CureCount.text = cures.ToString();
            anim.SetTrigger("Potion");
            health += 200;
            mainSlider.size = (float)health / Maxhealth;
            StartCoroutine(CureCoroutine());
        }
    }

    private void FixedUpdate()
    {
        //float h = Input.GetAxisRaw("Horizontal");
        float h = joystick.Horizontal;

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
            jumpSound.Play();
            rb.velocity = new Vector2(0, rb.velocity.y);
            Vector2 Ndirection = Vector2.right;
            transform.Translate(Ndirection.normalized * 45 * direction * Time.deltaTime);
            //rb.AddForce(Vector2.right * -10 * direction, ForceMode2D.Impulse);
            canRoll = false;
        }

        if (jump)
        {
            jumpSound.Play();
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
    public override void GetRing()
    {
        rings += 1;
        RingCount.text = rings.ToString();
        RingCountPause.text = rings.ToString();
    }

    public override void GetCure()
    {
        cures += 1;
        CureCount.text = cures.ToString();
    }


    public int GetHealth()
    {
        return health;
    }

    public override void TakeDamage(int damage, float directionVector)
    {
        if (canDamage)
        {
            canDamage = false;
            health -= damage;
            mainSlider.size = (float)health / Maxhealth;
            if (health <= 0)
            {
                anim.SetTrigger("Dead");
                StartCoroutine(RecarregaCena());
            }
            else
            {
                //anim.SetTrigger("Damage");
                StartCoroutine(DamageCoroutine(directionVector));

            }
        }
    }

    public void Death()
    {
        StartCoroutine(RecarregaCena());
    }

    public override void SetRings(int minus)
    {
        rings -= minus;
        RingCount.text = rings.ToString();
        RingCountPause.text = rings.ToString();
    }
    public override int GetRingCount()
    {
        return rings;
    }
    public override void SetMaxHealth(int healthExtra)
    {
        health += 100;
        Maxhealth += healthExtra;
    }

    public override void ImproveMana(int manaExtra)
    {
        if (mana < Maxmana)
        {
           mana += manaExtra;
           ManaSlider.size = (float)mana / Maxmana;
        }
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

    public IEnumerator RecarregaCena()
    {
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
            BlackFade.SetActive(true);
            Animator anim = BlackFade.GetComponent<Animator>();
            anim.SetTrigger("FadeOut");
            yield return new WaitForSeconds(1.1f);
            soudtrack.Stop();
            DeathScene.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            DeathScene.SetActive(false);
            yield return new WaitForSeconds(1f);
            int cenaAtual = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(cenaAtual);
        }
    }
    public override IEnumerator DamageCoroutine(float directionVector)
    {
        damageSound.Play();
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            yield return new WaitForSeconds(0.05f);
            rb.AddForce(Vector2.right * 5f * directionVector, ForceMode2D.Impulse);
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.5f);
           sprite.color = Color.white;
        }
        canDamage = true;
    }

    public IEnumerator ScenarioRoutine()
    {
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
            ScenarioName.SetActive(true);
            yield return new WaitForSeconds(5.5f);
            ScenarioName.SetActive(false);
            // yield return new WaitForSeconds(0.3f);
        }
    }

    public  IEnumerator CureCoroutine()
    {
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            sprite.color = Color.green;
            yield return new WaitForSeconds(0.5f);
            sprite.color = Color.white;
            // yield return new WaitForSeconds(0.3f);
        }
    }
}
