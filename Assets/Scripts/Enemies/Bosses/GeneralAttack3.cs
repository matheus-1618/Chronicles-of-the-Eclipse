using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAttack3 : MonoBehaviour
{
    private Animator anim;
    public int damage = 50;
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
        anim.Play("Attack3 Collider");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }

    }
}
