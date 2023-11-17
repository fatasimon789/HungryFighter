using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDameable 
{
    void Damage(float damageAmount);

    void die();

    float maxHealth { get; set; }

    float CurrentHeal { get; set; }
}
