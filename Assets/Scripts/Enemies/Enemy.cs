using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
/*    public float speed;
    public int health;
    public int damage;
    private Transform player;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 playerDistance;
    private bool facingRight;
    private bool isDead;
    private SpriteRenderer sprite;
    private bool move;*/

    public abstract void Flip();
    public abstract void TakeDamage(int damage);
    public abstract IEnumerator DamageCoroutine();
    public abstract IEnumerator StopRoutine();
    public abstract void DestroyEnemy();
   
}
