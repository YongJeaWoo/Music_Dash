using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using SingletonComponent.Component;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class NoteManager : SingletonComponent<NoteManager>
{
    private Note note;
    
    public string fileLocation;

    public bool IsMidiFileInitialized { get; private set; }

    #region Static Field

    public static MidiFile midiFile;

    #endregion

    private ObjectPoolManager objectPoolManager;
    private GameManager gameManager;

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

    public void StartInitMidiFile()
    {
        StartCoroutine(InitMidiFile());
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
        StartCoroutine(nameof(GenerateNotesFromMidi));
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

    IEnumerator GenerateNotesFromMidi()
    {
        var tempoMap = midiFile.GetTempoMap();
        var notes = midiFile.GetNotes();

        var noteFirstTime = TimeConverter.ConvertTo<MetricTimeSpan>(notes.First().Time, tempoMap);
        var startTime = noteFirstTime.TotalMicroseconds / 1000000.0;

        var defaultTime = Time.realtimeSinceStartup;

        foreach (var note in notes)
        {
            var noteOnTime = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, tempoMap);
            var noteOffTime = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time + note.Length, tempoMap);

            double noteStartTime = noteOnTime.TotalMicroseconds / 1000000.0;
            double noteEndTime = noteOffTime.TotalMicroseconds / 1000000.0;

            double elapsedTime = noteStartTime - startTime;

            yield return new WaitUntil(() => (Time.realtimeSinceStartup - defaultTime) >= elapsedTime);

            CreateNoteBasedOnData(note.NoteName.ToString(), note.NoteNumber, (float)noteStartTime, (float)(noteEndTime - noteStartTime));
        }
    }

    private void CreateNoteBasedOnData(string noteName, int noteNumber, float noteStartTime, float noteDuration)
    {
        string poolName = (noteName == "G") ? "UpperNote" : "UnderNote";
        GameObject noteObject = objectPoolManager.Create(poolName, null, gameManager.refreshParent.transform.position);
        Note noteComponent = noteObject.GetComponent<Note>();
        noteComponent.InitializeNoteData(noteName, noteNumber, noteStartTime, noteDuration);
        noteComponent.CheckYPos();
        noteObject.SetActive(true);
    }

    #endregion

    public Note GetNote() => note;

    public void InitNote(Note _note)
    {
        note = _note;
    }

    #endregion
}
