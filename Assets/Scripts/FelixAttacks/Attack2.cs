using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Attack2 : MonoBehaviour
{
    private Animator anim;
    public int damage = 40;
    private bool active = false;
    public Vector2 direction = Vector2.right;
    private float startTime;
    void Start(){}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            float distance = 7 * (Time.time - startTime);

            // Move the fire attack in the specified direction.
            transform.Translate(direction.normalized * distance * Time.deltaTime);

            // Check if the fire attack has been active for 5 seconds and destroy it.
            if (Time.time - startTime >= 2.5f)
            {
                active = false;
                Destroy(gameObject);
            }
        }
    }

    public void MagicBall(int attackDirection,int extra)
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
        anim.Play("FireBall");
        active = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

    }

}
