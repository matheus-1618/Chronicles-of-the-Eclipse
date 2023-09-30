using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2 : MonoBehaviour
{
    private Animator anim;
    private int damage = 40;
    private bool active = false;
    public Vector2 direction = Vector2.right;
    private float startTime;

    private FelixController felix;
    private Vector2 startPosition; // Store the starting position.
    private int directionAttack;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            float distance = 10 * (Time.time - startTime);

            // Move the fire attack in the specified direction.
            transform.Translate(direction.normalized * distance * Time.deltaTime);

            // Check if the fire attack has been active for 5 seconds and destroy it.
            if (Time.time - startTime >= 1.5f)
            {
                active = false;
                felix = GetComponentInParent<FelixController>();
                startPosition = new Vector2(felix.transform.position.x + 2f * directionAttack, felix.transform.position.y + 0.5f);
                transform.position = startPosition;
            }

        }

    }

    public void MagicBall(int direction)
    {
        directionAttack = direction;
        startTime = Time.time;
        anim.Play("attack2");
        active = false;
        
    }

}
