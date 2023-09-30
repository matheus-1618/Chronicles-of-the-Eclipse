using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Attack2 : MonoBehaviour
{
    private Animator anim;
    private int damage = 40;
    private bool active = false;
    public Vector2 direction = Vector2.right;
    private float startTime;
    void Start(){}

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
                Destroy(gameObject);
            }
        }
    }

    public void MagicBall(Vector3 playerScale)
    {
        transform.localScale = playerScale;
        anim = GetComponent<Animator>();
        startTime = Time.time;
        anim.Play("attack2");
        active = true;
    }

}
