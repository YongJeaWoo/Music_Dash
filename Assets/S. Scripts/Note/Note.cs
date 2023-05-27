using UnityEngine;

public class Note : MonoBehaviour
{
    private AudioManager audioManager;

    private double timeInstantiated;

    private Vector3 direction;

    public float assignedTime;

    private void OnEnable()
    {
        audioManager = AudioManager.Instance;
        NoteManager.Instance.InitNote(this);
        timeInstantiated = audioManager.GetAudioSourceTime();
    }

    private void Update()
    {
        MoveNote();
    }

    private void MoveNote()
    {
        double timeSinceInstantaited = audioManager.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantaited / (NoteManager.Instance.noteTime * 2));

        if (t > 1)
        {
            ObjectPoolManager.Instance.Return(gameObject);
        }
        else
        {
            transform.localPosition = 
                Vector3.Lerp(Vector3.right * NoteManager.Instance.noteSpawnX, Vector3.right * NoteManager.Instance.noteDespawnX, t);
        }
    }

    private void OnBecameInvisible()
    {
        ObjectPoolManager.Instance.Return(gameObject);
    }

    #region Virtual Method
    public virtual void CheckYPos() { }
    #endregion
}
