using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Walk", menuName = "Abillities/Walk")]
public class Walk : Abilities
{
    public override void Use(BossUnit owner, PlayerUnit target)
    {
        AbillityVisuals();
        _action.DoAction(owner, target);
    }
}
