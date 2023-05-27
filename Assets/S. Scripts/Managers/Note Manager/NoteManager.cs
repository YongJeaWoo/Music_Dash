using SingletonComponent.Component;
using System.IO;
using UnityEngine.Networking;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;
using System.Collections;

public class NoteManager : SingletonComponent<NoteManager>
{
    private Note note;

    public double marginOfError;

    public int inputDelayMillSec;

    public string fileLocation;

    public float songDelaySec;
    public float noteTime;
    public float noteSpawnX;
    public float noteTapX;

    public float noteDespawnX
    {
        get
        {
            return noteTapX - (noteSpawnX - noteTapX);
        }
    }

    #region Static Field

    public static MidiFile midiFile;

    #endregion

    #region Singleton

    protected override void AwakeInstance()
    {
        
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

    private void StartInitMidiFile()
    {
        if (Application.streamingAssetsPath.StartsWith("http://") || Application.streamingAssetsPath.StartsWith("Https://"))
        {
            StartCoroutine(ReadFromWebSite());
        }
        else
        {
            ReadFromFile();
        }
    }

    private IEnumerator ReadFromWebSite()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get($"{Application.streamingAssetsPath} / {fileLocation}"))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                byte[] results = webRequest.downloadHandler.data;
                using (var stream = new MemoryStream(results))
                {
                    midiFile = MidiFile.Read(stream);
                    GetDataFromMidi();
                }
            }
        }
    }

    private void ReadFromFile()
    {
        midiFile = MidiFile.Read($"{Application.streamingAssetsPath} / {fileLocation}");
        GetDataFromMidi();
    }

    public void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);

        Invoke(nameof(StartSong), songDelaySec);
    }

    public void StartSong()
    {
        AudioManager.Instance.Play();
    }

    #endregion

    public Note GetNote() => note;

    public void InitNote(Note _note)
    {
        note = _note;
    }

    #endregion
}
