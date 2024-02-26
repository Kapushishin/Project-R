using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTakeDamageBoss : ITakeDamageBoss
{
    public void OnTakeDamage(BossUnit boss, float damage)
    {
        boss._currentHealth -= damage;
    }
}
