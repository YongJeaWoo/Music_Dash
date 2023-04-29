using SingletonComponent.Component;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : SingletonComponent<ObjectPool>
{
    [SerializeField]
    private GameObject[] poolObjs;

    private List<Note>[] poolObjLists;

    private Note noteComponent;

    #region SingleTon
    protected override void AwakeInstance()
    {

    }

    protected override bool InitInstance()
    {
        noteComponent = poolObjs[0].GetComponent<Note>();
        poolObjLists = new List<Note>[poolObjs.Length];
        for (int i = 0; i < poolObjs.Length; i++)
        {
            poolObjLists[i] = new List<Note>();
        }
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
        for (int i = 0; i < poolObjs.Length; i++)
        {
            for (int j = 0; j < initCount; j++)
            {
                poolObjLists[i].Add(CreateNewObject(poolObjs[i]));
            }
        }
    }

    private Note CreateNewObject(GameObject obj)
    {
        var newObj = Instantiate(obj);
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj.GetComponent<Note>();
    }

    public static Note GetObject(int objIndex)
    {
        var instance = Instance;
        Note obj = null;

        if (instance.poolObjLists[objIndex].Count > 0)
        {
            obj = instance.poolObjLists[objIndex][0];
            instance.poolObjLists[objIndex].RemoveAt(0);
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = instance.CreateNewObject(instance.poolObjs[objIndex]);
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(null);
            instance.poolObjLists[objIndex].Add(obj);
        }

        return obj;
    }

    public static void ReturnObject(Note noteObj)
    {
        var instance = Instance;
        int objIndex = System.Array.IndexOf(instance.poolObjs, noteObj.gameObject);
        noteObj.gameObject.SetActive(false);
        noteObj.transform.SetParent(instance.transform);
        instance.poolObjLists[objIndex].Add(noteObj);
    }
}
