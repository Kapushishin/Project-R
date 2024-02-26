using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDoDamageBoss : IDoDamageBoss
{
    public void OnDoDamage(BossUnit boss)
    {
        if (Vector3.Distance(boss._playerUnit.transform.position, boss.transform.position) <= boss._range)
        {
            boss._playerUnit.TakeDamage(boss._damage);
        }
    }
}
