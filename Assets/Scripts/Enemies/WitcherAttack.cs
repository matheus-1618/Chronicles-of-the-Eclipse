using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitcherAttack : MonoBehaviour
{
    private Animator anim;
    public float distanceInitial;
    private int damage = 40;
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
            if (Time.time - startTime >= 2.5f)
            {
                active = false;
                Destroy(gameObject);
            }
        }
    }

    public void MagicBall(float attackDirection)
    {
        transform.localScale = new Vector3(2f, 2f, 1f);
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
            player.TakeDamage(damage);
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * (-5), ForceMode2D.Impulse);
            Destroy(gameObject);
        }
    }
}
