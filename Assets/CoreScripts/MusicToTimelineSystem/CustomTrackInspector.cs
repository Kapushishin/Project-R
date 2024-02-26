using UnityEditor;
using UnityEngine;


/* ��������� ��������� ��� ���������� ����� ���������.
 * ��������� 2 ������ � ���������� ��� ������ ����� BeatTrack �� ���������.
 * 
 */


[CustomEditor(typeof(BeatTrack))]
public class CustomTrackInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BeatTrack beatTrack = (BeatTrack)target;

        if (GUILayout.Button("Set Beats on Track"))
        {
            beatTrack.CreateBeatMark();
        }

        if (GUILayout.Button("Clear Beats"))
        {
            beatTrack.ClearBeats();
        }
    }
}
