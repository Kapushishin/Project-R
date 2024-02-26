using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;


/* Кастомный трек для таймлайна.
 * Содержит метод создания кастомных клипов на таймлайне в соответствии с BPM аудио.
 * Публичные методы вызываются из CustomTrackInspector
 */

//[TrackBindingType(typeof(ControlTrack))] // нужен для привязки трека к конкретному игровому объекту
[TrackClipType(typeof(BeatMarker))]
public class BeatTrack : TrackAsset
{
    #region Variables
    public float _bpm;
    public float _offset;
    public AudioClip _track;

    private TimelineClip _clip;
    private float _markerDuration = 0.05f;
    private int _beatsOnTrack;
    private float _quarterNoteTime;
    #endregion

    #region Public Methods
    public void CreateBeatMark()
    {
        _beatsOnTrack = GetTrackBeats();
        _quarterNoteTime = 60f / _bpm / 4f;

        float _previousNoteTime = 0;
        for (int i = 0; i < _beatsOnTrack; i++)
        {
            if (i % 4 == 0)
            {
                CreateNote(_previousNoteTime);
            }
            else
            {
                CreateQuarterNote(_previousNoteTime);
            }
            _previousNoteTime += _quarterNoteTime;
        }
    }

    public void ClearBeats()
    {
        foreach (TimelineClip clip in GetClips())
        {
            DeleteClip(clip);
        }
    }
    #endregion

    #region Internal Methods
    private int GetTrackBeats()
    {
        float beats = _track.length / (60f / _bpm / 4f);
        return (int)beats;
    }

    private void CreateQuarterNote(float _timeDistance)
    {
        _clip = CreateClip<BeatMarker>();
        _clip.duration = _markerDuration;
        _clip.start = _offset + _timeDistance;
    }

    private void CreateNote(float _timeDistance)
    {
        _clip = CreateClip<BeatMarker>();
        _clip.duration = _markerDuration * 2;
        _clip.start = _offset + _timeDistance;
    }
    #endregion
}
