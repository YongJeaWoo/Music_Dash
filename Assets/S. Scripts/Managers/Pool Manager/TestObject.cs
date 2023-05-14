using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ObjectPoolManager.Instance.Create("UpperNote");
        }
        if(Input.GetMouseButtonDown(1))
        {
            ObjectPoolManager.Instance.Create("UnderNote");
        }
        if(Input.GetMouseButtonDown(2))
        {
            ObjectPoolManager.Instance.ReturnAll("UpperNote");
            ObjectPoolManager.Instance.ReturnAll("UnderNote");
        }
    }
}
