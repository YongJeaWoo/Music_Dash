using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using SingletonComponent.Component;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class NoteManager : SingletonComponent<NoteManager>
{
    private Note note;

    public string fileLocation;

    public bool IsMidiFileInitialized { get; set; }

    #region Static Field

    public static MidiFile midiFile;

    #endregion

    private ObjectPoolManager objectPoolManager;
    private GameManager gameManager;

    private float judgeLine;

    private Judges upJudge;
    private Judges downJudge;

    #region Singleton

    protected override void AwakeInstance()
    {
        objectPoolManager = ObjectPoolManager.Instance;
        gameManager = GameManager.Instance;
    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {

    }

    #endregion

    #region Method

    #region Midifile

    public void ResetMidiFile()
    {
        StopCoroutine(nameof(InitMidiFile));
    }

    public void StartInitMidiFile()
    {
        StartCoroutine(nameof(InitMidiFile));
    }

    private IEnumerator InitMidiFile()
    {

        if (Application.streamingAssetsPath.StartsWith("http://") || Application.streamingAssetsPath.StartsWith("Https://"))
        {
            yield return ReadFromWebSite();
        }
        else
        {
            ReadFromFile();
        }

        IsMidiFileInitialized = true;

        yield return GenerateNotesFromMidi();
    }
    
    private IEnumerator ReadFromWebSite()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileLocation);

        if (File.Exists(filePath))
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            using (MemoryStream stream = new MemoryStream(fileBytes))
            {
                midiFile = MidiFile.Read(stream);
            }
        }
        else
        {
            Debug.LogError($"File Not Found: {filePath}");
        }

        yield return null;
    }

    private void ReadFromFile()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileLocation);

        if (File.Exists(filePath))
        {
            midiFile = MidiFile.Read(filePath);
        }
        else
        {
            Debug.LogError($"File Not Found : {filePath}");
        }
    }

    private IEnumerator GenerateNotesFromMidi()
    {
        var tempoMap = midiFile.GetTempoMap();
        var notes = midiFile.GetNotes();

        var noteFirstTime = TimeConverter.ConvertTo<MetricTimeSpan>(notes.First().Time, tempoMap);
        var startTime = noteFirstTime.TotalMicroseconds / 1000000.0;
        var defaultTime = 0f;

        var noteList = notes.ToList();

        for (var n = 0; n < noteList.Count; ++n)
        {
            var note = noteList[n];

            var noteOnTime = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, tempoMap);
            var noteOffTime = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time + note.Length, tempoMap);

            double noteStartTime = noteOnTime.TotalMicroseconds / 1000000.0;
            double noteEndTime = noteOffTime.TotalMicroseconds / 1000000.0;

            double elapsedTime = noteStartTime - startTime;

            while (defaultTime < elapsedTime)
            {
                defaultTime += Time.deltaTime;
                yield return null;
            }

            CreateNoteBasedOnData(n, note.NoteName.ToString(), note.NoteNumber, (float)(noteStartTime), (float)(noteEndTime - noteStartTime));

            yield return null;
        }
    }

    private void CreateNoteBasedOnData(int index, string noteName, int noteNumber, float noteStartTime, float noteDuration)
    {
        if (!IsMidiFileInitialized) return;

        var isUpJudge = noteName == "G";

        string poolName = isUpJudge ? "UpperNote" : "UnderNote";
        GameObject noteObject = objectPoolManager.Create(poolName, null, gameManager.refreshParent.transform.position);
        Note noteComponent = noteObject.GetComponent<Note>();
        noteComponent.InitializeNoteData(noteName, noteNumber, noteStartTime, noteDuration);
        noteComponent.CheckYPos();
        noteObject.SetActive(true);

        if (upJudge != null && isUpJudge)
        {
            upJudge.AddNoteToQueue(noteComponent);
        }

        if (downJudge != null && !isUpJudge)
        {
            downJudge.AddNoteToQueue(noteComponent);
        }

        if (index == 0)
        {
            float distance = Mathf.Abs(noteComponent.transform.position.x - judgeLine);
            float delay = (noteComponent.Speed != 0) ? Mathf.Max(0, (distance - Number.CHECK_DISTANCE) / noteComponent.Speed) : 0;
            StartCoroutine(MusicStart(delay));
        }
    }

    private IEnumerator MusicStart(float _delay)
    {
        yield return new WaitForSecondsRealtime(_delay);
        GameManager.Instance.MusicStart();
    }

    public void SetJudgement()
    {
        upJudge = gameManager.UpJudge;
        downJudge = gameManager.DownJudge;

        judgeLine = upJudge.transform.position.x;
    }

    public void ClearNotes()
    {
        GameObject[] notes = GameObject.FindGameObjectsWithTag("Note");

        for (int i = 0; i < notes.Length; i++)
        {
            objectPoolManager.Return(notes[i]);
        }
    }

    #endregion

    public void InitNote(Note _note)
    {
        note = _note;
    }

    #endregion
}