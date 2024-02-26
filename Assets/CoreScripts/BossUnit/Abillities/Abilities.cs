using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public abstract class Abilities : ScriptableObject
{
    [ShowAssetPreview(128, 128)] public ParticleSystem _particle;
    [Expandable] public Actions _action;
    public float _damage;

    public void AbillityVisuals()
    {
        _particle.Play();
    }

    public abstract void Use(BossUnit owner, PlayerUnit target);
}
