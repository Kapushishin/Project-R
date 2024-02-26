using UnityEditor;
using UnityEngine;


/* Кастомный инспектор для кастомного трека таймлайна.
 * Добавляет 2 кнопки в инспекторе при выборе трека BeatTrack на таймлайне.
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
