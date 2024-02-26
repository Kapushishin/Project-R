using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


/* ��������� ���� ��� ���������� ���������.
 * � BeatMarkerBehaviour ������� ��� �������� � ����������.
 * ������ ����� �� ����� ������.
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
