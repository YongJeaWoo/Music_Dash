using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class InfiniteScroll : UIBehaviour
{
    [SerializeField]
    private RectTransform itemPrototype;

    [SerializeField, Range(0, 30)]
    int instantateItemCount = 9;

    [SerializeField]
    private Direction direction;

    public OnItemPositionChange onUpdateItem = new OnItemPositionChange();

    [System.NonSerialized]
    public LinkedList<RectTransform> itemList = new LinkedList<RectTransform>();

    protected float diffPreFramePosition = 0;

    protected int selectedItemIndex = 0;

    public enum Direction
    {
        Vertical,
        Horizontal,
    }

    private RectTransform rectTransform;
    protected RectTransform RectTransform
    {
        get
        {
            if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
            return rectTransform;
        }
    }

    private float anchoredPosition
    {
        get
        {
            return direction == Direction.Vertical ? -RectTransform.anchoredPosition.y : RectTransform.anchoredPosition.x;
        }
    }

    private float itemScale = -1;
    public float ItemScale
    {
        get
        {
            if (itemPrototype != null && itemScale == -1)
            {
                itemScale = direction == Direction.Vertical ? itemPrototype.sizeDelta.y: itemPrototype.sizeDelta.x;
            }
            return itemScale;
        }
    }

    private RectTransform SelectedItem
    {
        get
        {
            return itemList.ElementAt(selectedItemIndex);
        }
    }

    protected override void Start()
    {
        var controllers = GetComponents<MonoBehaviour>()
                .Where(item => item is IInfiniteScrollSetup)
                .Select(item => item as IInfiniteScrollSetup)
                .ToList();

        var scrollRect = GetComponentInParent<ScrollRect>();
        scrollRect.horizontal = direction == Direction.Horizontal;
        scrollRect.vertical = direction == Direction.Vertical;
        scrollRect.content = RectTransform;

        itemPrototype.gameObject.SetActive(false);

        for (int i = 0; i < instantateItemCount; i++)
        {
            var item = GameObject.Instantiate(itemPrototype) as RectTransform;
            item.SetParent(transform, false);
            item.name = i.ToString();
            item.anchoredPosition = direction == Direction.Vertical ? new Vector2(0, (-ItemScale * i) - 110f) : new Vector2(ItemScale * i, 0);
            itemList.AddLast(item);

            item.gameObject.SetActive(true);

            foreach (var controller in controllers)
            {
                controller.OnUpdateItem(i, item.gameObject);
            }
        }

        foreach (var controller in controllers)
        {
            controller.OnPostSetupItems();
        }
    }

    void Update()
    {
        Check();
    }

    private void Check()
    {
        if (itemList.First == null)
        {
            return;
        }

        while (anchoredPosition - diffPreFramePosition < -ItemScale * 2)
        {
            diffPreFramePosition -= ItemScale;

            var item = itemList.First.Value;
            itemList.RemoveFirst();
            itemList.AddLast(item);

            var pos = ItemScale * instantateItemCount + ItemScale * selectedItemIndex;
            item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);

            onUpdateItem.Invoke(selectedItemIndex + instantateItemCount, item.gameObject);

            selectedItemIndex++;
        }

        while (anchoredPosition - diffPreFramePosition > 0)
        {
            diffPreFramePosition += ItemScale;

            var item = itemList.Last.Value;
            itemList.RemoveLast();
            itemList.AddFirst(item);

            selectedItemIndex--;

            var pos = ItemScale * selectedItemIndex;
            item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);
            onUpdateItem.Invoke(selectedItemIndex, item.gameObject);
        }
    }

    private void SelectItem(int index)
    {
        selectedItemIndex = index;

        float offset = GetoffsetToCenter();
        RectTransform.anchoredPosition -= (direction == Direction.Vertical) ? new Vector2(0, offset) : new Vector2(offset, 0);
    }

    private float GetoffsetToCenter()
    {
        var center = (direction == Direction.Vertical) ? RectTransform.rect.height / 2 : RectTransform.rect.width / 2;

        var selectItem = SelectedItem;
        var pos = selectItem.localPosition;

        if (direction == Direction.Vertical)
        {
            pos.y += RectTransform.anchoredPosition.y;
        }
        else
        {
            pos.x -= RectTransform.anchoredPosition.x;
        }

        var diff = center - pos.y;
        return diff;
    }

    private void HandleKeyboardInput()
    {
        if (itemList.First == null) return;

        if (EventSystem.current.currentSelectedGameObject == null) SelectItem(0);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectedItemIndex > 0)
            {
                selectedItemIndex--;
                SelectItem(selectedItemIndex);
            }
            else
            {
                selectedItemIndex = itemList.Count - 1;
                SelectItem(selectedItemIndex);
            }
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectedItemIndex < itemList.Count - 1)
            {
                selectedItemIndex++;
                SelectItem(selectedItemIndex);
            }
            else
            {
                selectedItemIndex = 0;
                SelectItem(selectedItemIndex);
            }
        }
    }

    [System.Serializable]
    public class OnItemPositionChange : UnityEngine.Events.UnityEvent<int, GameObject> { }
}
