using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerAttack1 : MonoBehaviour
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

    public void Blade(float attackIntensity)
    {
        anim = GetComponent<Animator>();
        startTime = Time.time;
        anim.Play("Attack1 Collider");

        // Mover o objeto +5 unidades para a direita
        Vector3 currentPosition = transform.position; // A posição atual do objeto
        Vector3 newPosition = currentPosition + new Vector3(-attackIntensity, 0f, 0f); // Nova posição com +5 unidades à direita
        transform.position = newPosition; // Definir a nova posição do objeto
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
