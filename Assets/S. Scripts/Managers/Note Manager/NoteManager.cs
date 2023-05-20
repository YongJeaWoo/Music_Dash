using SingletonComponent.Component;

public class NoteManager : SingletonComponent<NoteManager>
{
    #region Private Field

    private Note note = null;

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

    public Note GetNote() => note;

    public Note SetNote(Note _note) => note = _note;

    #endregion
}
