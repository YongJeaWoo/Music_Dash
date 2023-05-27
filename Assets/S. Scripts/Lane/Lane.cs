using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public GameObject notePrefab;

    public List<double> timeStamps = new List<double>(); 

    private int spawnIndex = 0;
    private int inputIndex = 0;

    private void Update()
    {
        Test();
    }

    private void Test()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (AudioManager.Instance.GetAudioSourceTime() >= timeStamps[spawnIndex] - NoteManager.Instance.noteTime)
            {

            }
        }
    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] _array)
    {
        foreach (var note in _array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, NoteManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }
}
