using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Attack1 : MonoBehaviour
{
    private Animator anim;
    public int damage = 50;
    public Vector2 direction = Vector2.right;
    private float startTime;
    void Start(){}

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if the fire attack has been active for 5 seconds and destroy it.
        if (Time.time - startTime >= 1.5f)
        {
            //Destroy(gameObject);
        }
        
    }

    public int GetDamage()
    {
        return damage;
    }

    public void Explosion(int extra)
    {
        damage += extra;
        anim = GetComponent<Animator>();
        startTime = Time.time;
        anim.Play("Attack1Collider");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

    }

}
