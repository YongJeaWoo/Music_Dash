using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private GameObject m_targetObject = null;

    private Transform m_activeObject = null;
    private Transform m_deactiveObject = null;

    private List<GameObject> m_activePoolList = new List<GameObject>();
    private List<GameObject> m_deactivePoolList = new List<GameObject>();

    public void Initialize(GameObject _target)
    {
        m_targetObject = _target;

        m_activeObject = new GameObject("Active").transform;
        m_activeObject.SetParent(transform);

        m_deactiveObject = new GameObject("Deactive").transform;
        m_deactiveObject.SetParent(transform);
        m_deactiveObject.gameObject.SetActive(false);
    }

    public GameObject Create(Transform _parent = null)
    {
        _parent ??= m_activeObject;

        if(m_deactivePoolList.Count <= 0)
        {
            var ins = Instantiate(m_targetObject, _parent);
            ins.name = m_targetObject.name;
            m_activePoolList.Add(ins);
            return ins;
        }

        var obj = m_deactivePoolList.Last();
        obj.transform.SetParent(_parent);
        m_deactivePoolList.Remove(obj);
        m_activePoolList.Add(obj);
        return obj;
    }

    public GameObject Create(Transform _parent, Vector3 _position)
    {
        var obj = Create(_parent);
        obj.transform.position = _position;
        return obj;
    }

    public void Return(GameObject _obj)
    {
        var find = m_activePoolList.Find(x => x.Equals(_obj));

        if(null != find)
        {
            m_activePoolList.Remove(_obj);
            m_deactivePoolList.Add(_obj);
            _obj.transform.SetParent(m_deactiveObject);
        }
    }

    public void ReturnAll()
    {
        for(var i = m_activePoolList.Count - 1; i >= 0; --i)
        {
            var obj = m_activePoolList[i];
            m_activePoolList.RemoveAt(i);

            m_deactivePoolList.Add(obj);
            obj.transform.SetParent(m_deactiveObject);
        }
    }
}
