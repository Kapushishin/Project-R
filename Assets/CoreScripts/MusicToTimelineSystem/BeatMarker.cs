using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


/* Кастомный клип для кастомного таймлайна.
 * В BeatMarkerBehaviour описаны его свойства и переменные.
 * Данные клипы не умеют ничего.
 */

public class BeatMarker : PlayableAsset, ITimelineClipAsset
{
    [SerializeField]
    private BeatMarkerBehaviour template = new();

    public ClipCaps clipCaps
    { get { return ClipCaps.None; } }


    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<BeatMarkerBehaviour>.Create(graph, template);
    }
}
