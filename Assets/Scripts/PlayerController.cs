using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public abstract void Flip();
    public abstract void TakeDamage(int damage, float directionVector);
    public abstract IEnumerator DamageCoroutine(float directionVector);

    public abstract void GetRing();

    public abstract void GetCure();

    public abstract void SetMaxHealth(int health);
    public abstract void SetattackImprovement(int extra);
    public abstract void SetDodgeTime(float minus);

    public abstract void SetRings(int minus);
    public abstract int GetRingCount();
}
