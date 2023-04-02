using UnityEngine;

namespace DTT.InfiniteScroll
{
    public class ScrollController : MonoBehaviour
    {
        [SerializeField]
        private GameObject musicPrefab;

        [SerializeField]
        private Transform contentTransform;

        private InfiniteScroll infiniteScroll;

        private int selectIndex = 7;

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

            for (int i = 0; i < selectIndex; i++)
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
            infiniteScroll.Next();
            Debug.Log($"Target : {infiniteScroll.Target}");
        }

        private void ScrollDown()
        {
            infiniteScroll.Previous();
            Debug.Log($"Target : {infiniteScroll.Target}");
        }
    }
}