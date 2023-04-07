using UnityEngine;
using UnityEngine.UI;

namespace DTT.InfiniteScroll
{
    public class ScrollController : MonoBehaviour
    {
        [SerializeField]
        private GameObject musicPrefab;

        [SerializeField]
        private Transform contentTransform;

        private Button lastSelectedButton;

        private InfiniteScroll infiniteScroll;

        private int selectingIndex = 7;

        private void Start()
        {
            CheckInit();
        }

        private void Update()
        {
            InputKeys();
        }

        private void CheckInit()
        {
            infiniteScroll = GetComponent<InfiniteScroll>();

            if (musicPrefab == null) return;

            for (int i = 0; i < selectingIndex; i++)
            {
                var obj = Instantiate(musicPrefab);
                obj.name = $"Music{i}";
                obj.transform.SetParent(contentTransform);
            }
        }

        private void InputKeys()
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                ScrollDown();
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                ScrollUp();
            }
        }

        private void ScrollUp()
        {
            if (lastSelectedButton != null) lastSelectedButton.targetGraphic.color = Color.green;

            infiniteScroll.Previous();
            SetPoint();
        }

        private void ScrollDown()
        {
            if (lastSelectedButton != null) lastSelectedButton.targetGraphic.color = Color.green;

            infiniteScroll.Next();
            SetPoint();
        }

        private void SetPoint()
        {
            Button targetButton = infiniteScroll.Target.GetComponent<Button>();
            targetButton.targetGraphic.color = Color.white;
            lastSelectedButton = targetButton;
        }
    }
}