using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Burn<T> : StatusEffect
{
    Tick frequency;
    float damage;
    GameObject owner;
    GameObject victim;

    public SE_Burn(float freq, float dmg, GameObject original, GameObject target) => (frequency, damage, owner, victim) = (new Tick(freq, true), dmg, original, target);

    protected override void Effect()
    {
        // (victim.GetComponent("PlayerScript") as DamageableEntity).TakeDamage(damage);
        // TODO: Implemenet DamageableEntity superclass for Player + Enemies, or make something similar and change this line to fit.
    }
}
