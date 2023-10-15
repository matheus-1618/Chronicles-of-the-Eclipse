using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerAttack2 : MonoBehaviour
{
    private Animator anim;
    private int damage = 50;
    public Vector2 direction = Vector2.right;
    private float startTime;
    private bool active = false;
    void Start() { }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            float distance = 7 * (Time.time - startTime);

            // Move the fire attack in the specified direction.
            transform.Translate(direction.normalized * distance * Time.deltaTime);

            // Check if the fire attack has been active for 5 seconds and destroy it.
            if (Time.time - startTime >= 3f)
            {
                active = false;
                Destroy(gameObject);
            }
        }

    }

    public int GetDamage()
    {
        return damage;
    }

    public void Blade(float attackDirection)
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        if (attackDirection == 1f)
        {
            direction = Vector2.right;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        else
        {
            direction = Vector2.left;
        }
        anim = GetComponent<Animator>();
        startTime = Time.time;
        active = true;
        anim = GetComponent<Animator>();
        startTime = Time.time;
        anim.Play("Attack2 Collider");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}
