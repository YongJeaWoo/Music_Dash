using SingletonComponent.Component;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : SingletonComponent<ObjectPoolManager>
{
    #region Serialize Field

    [SerializeField] List<GameObject> m_poolObjects = null;

    #endregion

    #region Private Field

    private Dictionary<string, ObjectPool> m_objectPoolDics = new Dictionary<string, ObjectPool>();

    #endregion

    #region Singleton
    protected override void AwakeInstance()
    {
        Initialize();
    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {

    }
    #endregion

    #region Initialize

    public void Initialize()
    {
        m_poolObjects.ForEach(CreateObjectPool);
    }

    private void CreateObjectPool(GameObject _obj)
    {
        var obj = new GameObject($"{_obj.name}_Pool");
        obj.transform.SetParent(transform);
        var pool = obj.AddComponent<ObjectPool>();
        m_objectPoolDics.Add(_obj.name, pool);
        pool.Initialize(_obj);
    }

    #endregion

    #region Method

    public ObjectPool GetPool(string _poolName)
    {
        return m_objectPoolDics.TryGetValue(_poolName, out var result) ? result : null;
    }

    public GameObject Create(string _poolName, Transform _parent = null)
    {
        var pool = GetPool(_poolName);

        if(pool == null)
        {
            Debug.LogError($"{_poolName}에 대한 오브젝트 풀이 존재하지 않습니다.");
            return null;
        }

        return pool.Create(_parent);
    }

    public void Return(GameObject _obj)
    {
        var pool = GetPool(_obj.name);

        if (pool == null)
        {
            Debug.LogError($"{_obj.name}에 대한 오브젝트 풀이 존재하지 않습니다.");
            return;
        }

        pool.Return(_obj);
    }

    public void Return(string _poolName, GameObject _obj)
    {
        var pool = GetPool(_poolName);

        if (pool == null)
        {
            Debug.LogError($"{_poolName}에 대한 오브젝트 풀이 존재하지 않습니다.");
            return;
        }

        pool.Return(_obj);
    }

    public void ReturnAll(string _poolName)
    {
        var pool = GetPool(_poolName);

        if (pool == null)
        {
            Debug.LogError($"{_poolName}에 대한 오브젝트 풀이 존재하지 않습니다.");
            return;
        }

        pool.ReturnAll();
    }

    #endregion
}
