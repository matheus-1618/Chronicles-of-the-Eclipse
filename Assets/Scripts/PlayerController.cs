using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void Flip();
    public abstract void TakeDamage(int damage);
    public abstract IEnumerator DamageCoroutine();
}
