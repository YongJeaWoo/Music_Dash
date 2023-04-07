using SingletonComponent.Component;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : SingletonComponent<PopupManager>
{
    [SerializeField]
    private Transform canvasParent = null;

    const string PATH = "Prefabs";

    private List<Component> popUps = new List<Component>();

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

    public T GetPopUp<T>() where T : Component
    {
        var res = Resources.Load<T>($"{PATH}/{typeof(T).Name}");
        if (res == null)
        {
            Debug.LogError($"{nameof(PopupManager)} : GetPopup Error");
            return null;
        }
        var popUp = Instantiate(res);
        popUp.transform.SetParent(canvasParent);
        popUps.Add(popUp);
        return popUp;
    }

    public T Find<T>() where T : Component
    {
        return popUps.Find(x => x.GetType() == typeof(T)) as T;
    }

    public bool IsUsePopup<T>() where T : Component
    {
        int index = popUps.FindIndex(x => x.GetType() == typeof(T));
        return index >= 0;
    }

    public void RemovePopUp<T>() where T : Component
    {
        int index = popUps.FindIndex(x => x.GetType() == typeof(T));

        if (index >= 0)
        {
            // popUps[index].gameObject.SetActive(false);
            Destroy(popUps[index].gameObject);
            popUps.RemoveAt(index);
        }
    }
}
