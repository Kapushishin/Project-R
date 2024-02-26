using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamageBoss
{
    public void OnTakeDamage(BossUnit boss, float damage);
}
