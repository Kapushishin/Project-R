using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;


[Serializable]
public class AbillityMarkerBehavoiur : PlayableBehaviour
{
    private BossUnit _boss;
    private bool _canDo = true;
    [Expandable] public Abilities _abillity;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        base.ProcessFrame(playable, info, playerData);
        
        if (_canDo)
        {
            _canDo = false;
            _boss = playerData as BossUnit;
            _boss.UseAbillity(_abillity);
        } 
    }
}
