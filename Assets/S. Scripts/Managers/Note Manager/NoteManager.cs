using SingletonComponent.Component;
using UnityEngine;

public class NoteManager : SingletonComponent<NoteManager>
{
    private Note note;

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

    public Note GetNote() => note;

    public void InitNote(Note _note)
    {
        note = _note;
    }

    #endregion
}
