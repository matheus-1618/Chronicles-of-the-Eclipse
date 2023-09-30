using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2 : MonoBehaviour
{
    public float speed = 5f; // The speed at which the fire attack moves.
    public Vector2 direction = Vector2.right; // The initial direction of the fire attack.
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        /*float distance = speed * (Time.time - startTime);

        // Move the fire attack in the specified direction.
        transform.Translate(direction.normalized * distance * Time.deltaTime);*/
    }
}
