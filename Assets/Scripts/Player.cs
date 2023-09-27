using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float maxSpeed = 5;
    private float speed;
    private bool facingRight = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = maxSpeed;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(h * speed, rb.velocity.y);

        if ((h > 0 && !facingRight) || (h < 0 && facingRight))
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
