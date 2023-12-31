using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AriaAttack2 : MonoBehaviour
{
    private Animator anim;
    public int damage = 40;
    private bool active = false;
    public Vector2 direction = Vector2.right;
    private float startTime;
    void Start() { }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            float distance = 12 * (Time.time - startTime);

            // Move the fire attack in the specified direction.
            transform.Translate(direction.normalized * distance * Time.deltaTime);

            // Check if the fire attack has been active for 5 seconds and destroy it.
            if (Time.time - startTime >= 1.2f)
            {
                active = false;
                Destroy(gameObject);
            }
        }
    }

    public void MagicBall(int attackDirection, int extra)
    {
        damage += extra;
        transform.localScale = new Vector3(4f, 4f, 1f);
        if (attackDirection == 1)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        anim = GetComponent<Animator>();
        startTime = Time.time;
        anim.Play("LightBall");
        active = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            GameObject playercontroller = GameObject.FindWithTag("Player");
            PlayerController player = playercontroller.GetComponent<PlayerController>();
            player.ImproveMana(50);
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

    }

}
