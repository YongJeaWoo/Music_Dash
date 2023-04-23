using SingletonComponent.Component;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : SingletonComponent<ObjectPool>
{
    [SerializeField]
    private GameObject poolObj;

    Queue<Note> poolObjQueue = new Queue<Note>();

    #region SingleTon
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

    private void Start()
    {
        Initialize(30);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolObjQueue.Enqueue(CreateNewObject());
        }
    }

    private Note CreateNewObject()
    {
        var newObj = Instantiate(poolObj).GetComponent<Note>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static Note GetObject()
    {
        if (Instance.poolObjQueue.Count > 0)
        {
            var obj = Instance.poolObjQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public static void ReturnObject(Note noteObj)
    {
        noteObj.gameObject.SetActive(false);
        noteObj.transform.SetParent(Instance.transform);
        Instance.poolObjQueue.Enqueue(noteObj);
    }
}
