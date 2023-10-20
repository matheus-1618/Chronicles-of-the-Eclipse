using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastAttack : MonoBehaviour
{
    private Animator anim;
    public float distanceInitial;
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
            float distance = distanceInitial * (Time.time - startTime);

            // Move the fire attack in the specified direction.
            transform.Translate(direction.normalized * distance * Time.deltaTime);

            // Check if the fire attack has been active for 5 seconds and destroy it.
            if (Time.time - startTime >= 2f)
            {
                active = false;
                Destroy(gameObject);
            }
        }
    }

    public void MagicBall(float attackDirection)
    {
        transform.localScale = new Vector3(4f, 4f, 1f);
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
        anim.Play("FireBall");
        active = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            float directionVector = (player.transform.position.x - transform.position.x) / Mathf.Abs(player.transform.position.x - transform.position.x);
            player.TakeDamage(damage, directionVector);
            Destroy(gameObject);
        }
    }
}
