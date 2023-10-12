using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    public abstract void Flip();
    public abstract void TakeDamage(int damage);
    public abstract IEnumerator DamageCoroutine();
    public abstract IEnumerator StopRoutine();
    public abstract void DestroyEnemy();
   
}
