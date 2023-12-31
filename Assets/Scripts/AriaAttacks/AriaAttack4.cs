using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AriaAttack4 : MonoBehaviour
{
    private Animator anim;
    public int damage = 120;
    public Vector2 direction = Vector2.right;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Blade(int extra)
    {
        damage += extra;
        anim = GetComponent<Animator>();
        startTime = Time.time;
        anim.Play("Attack4Collider");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            PlayerController player = GetComponentInParent<PlayerController>();
            player.ImproveMana(50);
            enemy.TakeDamage(damage);
        }

    }
}
