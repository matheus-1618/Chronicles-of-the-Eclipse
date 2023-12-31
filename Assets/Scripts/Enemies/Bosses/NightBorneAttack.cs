using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightBorneAttack : MonoBehaviour
{
    private Animator anim;
    public int damage = 150;
    public Vector2 direction = Vector2.right;
    private float startTime;
    void Start() { }

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

    public void Blade()
    {
        anim = GetComponent<Animator>();
        startTime = Time.time;
        anim.Play("Attack Collider");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            float directionVector = 1.5f*(player.transform.position.x - transform.position.x) / Mathf.Abs(player.transform.position.x - transform.position.x);
            player.TakeDamage(damage, directionVector);
        }

    }
}
