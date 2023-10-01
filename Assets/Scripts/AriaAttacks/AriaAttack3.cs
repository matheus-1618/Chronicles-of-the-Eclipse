using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AriaAttack3 : MonoBehaviour
{
    private Animator anim;
    private int damage = 120;
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

    public void Blade()
    {
        anim = GetComponent<Animator>();
        startTime = Time.time;
        anim.Play("AriaAttack3");
    }
}
