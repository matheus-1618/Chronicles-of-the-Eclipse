using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private float time;
    private bool direction = false;
    void Start()
    {
        time = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - time > 0.1f && direction)
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 0f);
            time = Time.time;
            direction = false;
        }
        else if (Time.time - time > 0.1f && !direction)
        {
            transform.localScale = new Vector3(0.35f, 0.35f, 0f);
            time = Time.time;
            direction = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.GetRing();
            Destroy(gameObject);
        }

    }

}
