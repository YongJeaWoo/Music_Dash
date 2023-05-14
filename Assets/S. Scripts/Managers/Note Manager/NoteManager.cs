using SingletonComponent.Component;
using UnityEngine;

public class NoteManager : SingletonComponent<NoteManager>
{
    #region Private Field

    private Note note = null;
    private Bounds bounds;

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

    private void Update()
    {
        ReturnNote();
    }

    public Note SetNote(Note _note) => note = _note;
    public Bounds SetBounds(Note _note) => bounds = SetNote(note).Bounds;

    private void ReturnNote()
    {
        if (Camera.main.WorldToScreenPoint(transform.position + bounds.extents.x * Vector3.right).x < 0f)
        {
            // TODO : 노트 비활성화
            ObjectPoolManager.Instance.Return();
        }
    }

    #endregion
}
