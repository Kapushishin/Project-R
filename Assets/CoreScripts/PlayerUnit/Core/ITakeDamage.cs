using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage
{
    public void OnTakeDamage(PlayerUnit player, float damage);
}
