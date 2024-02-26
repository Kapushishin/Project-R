using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class AbillityMarker : PlayableAsset, ITimelineClipAsset
{
    [SerializeField]
    private AbillityMarkerBehavoiur template = new();

    public ClipCaps clipCaps
    { get { return ClipCaps.Blending; } }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<AbillityMarkerBehavoiur>.Create(graph, template);
    }
}
