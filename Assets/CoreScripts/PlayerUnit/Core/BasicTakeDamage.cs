using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTakeDamage : ITakeDamage
{
    public void OnTakeDamage(PlayerUnit player, float damage)
    {
        if (player._canTakeDamage)
        {
            //take damage;
        }
    }
}
