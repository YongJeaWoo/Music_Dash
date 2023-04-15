using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private int noteType;
    public int NoteType
    {
        get => noteType;
        set => noteType = value;
    }

    private float noteSpeed = 2f;

    private void Update()
    {
        NoteMoving();
    }

    private void NoteMoving()
    {
        transform.Translate(Vector2.left * noteSpeed * Time.deltaTime);
    }
}
