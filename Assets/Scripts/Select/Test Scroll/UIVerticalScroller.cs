using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions
{
    [RequireComponent(typeof(ScrollRect))]

    public class UIVerticalScroller : MonoBehaviour
    {
        public ScrollRect scrollRect;

        public RectTransform center;
        public RectTransform elementSize;

        public Vector2 elementShrinkage = new Vector2(1f / 200, 1f / 200);
        public Vector2 minScale = new Vector2(0.7f, 0.7f);

        public int startingIndex = -1;

        public bool stopMomentumOnEnd = true;
        public bool disableUnFocused = true;

        public IntEvent OnButtonClicked;
        public IntEvent OnFocusChanged;

        [HideInInspector]
        public GameObject[] arrayOfElements;

        public int FocusedElementIndex { get; private set; }

        public string Result { get; private set; }

        private float[] distReposition;
        private float[] distance;

        private float coolTime = 1f;
        private bool isCoolDown;

        [HideInInspector]
        public RectTransform ScrollingPanel { get => scrollRect.content; }

        public UIVerticalScroller() { }

        public UIVerticalScroller(RectTransform center, RectTransform elementSize, ScrollRect scrollRect, GameObject[] arrayOfElements)
        {
            this.center = center;
            this.elementSize = elementSize;
            this.scrollRect = scrollRect;
            this.arrayOfElements = arrayOfElements;
        }

        public void Awake()
        {
            if (!scrollRect) scrollRect = GetComponent<ScrollRect>();
            if (!center) Debug.LogError("RectTransform Error");
            if (!elementSize) elementSize = center;
            if (arrayOfElements == null || arrayOfElements.Length == 0)
            {
                arrayOfElements = new GameObject[ScrollingPanel.childCount];

                for (int i = 0; i < ScrollingPanel.childCount; i++)
                {
                    arrayOfElements[i] =ScrollingPanel.GetChild(i).gameObject;
                }
            }
        }

        public void CreateObject(int startingIndex = -1, GameObject[] _arrayOfElements = null)
        {
            if (_arrayOfElements != null)
            {
                arrayOfElements = _arrayOfElements;
            }
            else
            {
                arrayOfElements = new GameObject[ScrollingPanel.childCount];

                for (int i = 0; i < ScrollingPanel.childCount; i++)
                {
                    arrayOfElements[i] = ScrollingPanel.GetChild(i).gameObject;
                }
            }

            for (var i = 0; i < arrayOfElements.Length; i++)
            {
                int j = i;
                arrayOfElements[i].GetComponent<Button>().onClick.RemoveAllListeners();
                
                if (OnButtonClicked != null)
                {
                    arrayOfElements[i].GetComponent<RectTransform>();
                }
                RectTransform rt = arrayOfElements[i].GetComponent<RectTransform>();
                rt.anchorMax = rt.anchorMin = rt.pivot = new Vector2(0.5f, 0.5f);
                rt.localPosition = new Vector2(0, i * elementSize.rect.size.y);
                rt.sizeDelta = elementSize.rect.size;
            }

            distance = new float[arrayOfElements.Length]; 
            distReposition = new float[arrayOfElements.Length];
            FocusedElementIndex = -1;

            if (startingIndex > -1)
            {
                startingIndex = startingIndex > arrayOfElements.Length ? arrayOfElements.Length - 1 : startingIndex;
                SnapToElement(startingIndex);
            }
        }

        public void UpdateInput()
        {
            if (arrayOfElements.Length < 1) return;

            for (var i = 0; i < arrayOfElements.Length; i++)
            {
                distReposition[i] = center.GetComponent<RectTransform>().position.y - arrayOfElements[i].GetComponent<RectTransform>().position.y;
                distance[i] = Mathf.Abs(distReposition[i]);

                Vector2 scale = Vector2.Max(minScale, new Vector2(1 / (1 + distance[i] * elementShrinkage.x), (1 / (1 + distance[i] * elementShrinkage.y))));
                arrayOfElements[i].GetComponent<RectTransform>().transform.localScale = new Vector3(scale.x, scale.y, 1f);
            }

            float minDistance = Mathf.Min(distance);
            int oldFocusedElement = FocusedElementIndex;
            for (var i = 0; i < arrayOfElements.Length; i++)
            {
                arrayOfElements[i].GetComponent<CanvasGroup>().interactable = !disableUnFocused || minDistance == distance[i];
                if (minDistance == distance[i])
                {
                    FocusedElementIndex = i;
                    Result = arrayOfElements[i].GetComponentInChildren<Text>().text;
                }
            }
            if (FocusedElementIndex != oldFocusedElement && OnFocusChanged != null)
            {
                OnFocusChanged.Invoke(FocusedElementIndex);
            }

            if (UIInputManager.GetKey(KeyCode.UpArrow) && !isCoolDown)
            {
                isCoolDown = true;
                StartCoroutine(ScrollUp(coolTime));
            }

            if (UIInputManager.GetKey(KeyCode.DownArrow) && !isCoolDown)
            {
                isCoolDown = true;
                StartCoroutine(ScrollDown(coolTime));
            }

            if (stopMomentumOnEnd
                && (arrayOfElements[0].GetComponent<RectTransform>().position.y > center.position.y
                || arrayOfElements[arrayOfElements.Length - 1].GetComponent<RectTransform>().position.y < center.position.y))
            {
                scrollRect.velocity = Vector2.zero;
            }
        }

        //private void ScrollingElements()
        //{
        //    float newY = Mathf.Lerp(ScrollingPanel.anchoredPosition.y, ScrollingPanel.anchoredPosition.y + distReposition[FocusedElementIndex], Time.deltaTime * 2f); ;
        //    Vector2 newPosition = new Vector2(ScrollingPanel.anchoredPosition.x, newY);
        //    ScrollingPanel.anchoredPosition = newPosition;
        //}

        public void SnapToElement(int element)
        {
            float deltaElementPositionY = elementSize.rect.height * element;
            Vector2 newPosition = new Vector2(ScrollingPanel.anchoredPosition.x, -deltaElementPositionY);
            ScrollingPanel.anchoredPosition = newPosition;
        }

        private IEnumerator ScrollUp(float time)
        {
            yield return new WaitForSeconds(time);
            float deltaUp = elementSize.rect.height / 1.2f;
            Vector2 newPositionUp = new Vector2(ScrollingPanel.anchoredPosition.x, ScrollingPanel.anchoredPosition.y - deltaUp);
            ScrollingPanel.anchoredPosition = Vector2.Lerp(ScrollingPanel.anchoredPosition, newPositionUp, 1);
            isCoolDown = false;
        }

        private IEnumerator ScrollDown(float time)
        {
            yield return new WaitForSeconds(time);
            float deltaDown = elementSize.rect.height / 1.2f;
            Vector2 newPositionDown = new Vector2(ScrollingPanel.anchoredPosition.x, ScrollingPanel.anchoredPosition.y + deltaDown);
            ScrollingPanel.anchoredPosition = newPositionDown;
            isCoolDown = false;
        }

        [System.Serializable]
        public class IntEvent : UnityEvent<int> { }
    }
}