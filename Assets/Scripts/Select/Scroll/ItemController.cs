using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InfiniteScroll))]
public class ItemController : UIBehaviour, IInfiniteScrollSetup
{
    private bool isSetup = false;

    public void OnPostSetupItems()
    {
        GetComponentInParent<ScrollRect>().movementType = ScrollRect.MovementType.Unrestricted;
        isSetup = true;
    }

    public void OnUpdateItem(int itemCount, GameObject obj)
    {
        if (isSetup) return;

        var item = obj.GetComponentInChildren<Music>();
        item.UpdateItem(itemCount);
    }
}
