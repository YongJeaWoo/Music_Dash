using System.Linq;
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
            infiniteScroll.Previous();
            SetPoint();
        }

        private void ScrollDown()
        {
            infiniteScroll.Next();
            SetPoint();
        }

        private void SetPoint()
        {
            foreach (var button in contentTransform.GetComponentsInChildren<Button>())
            {
                if (button == infiniteScroll.Target.GetComponent<Button>())
                {
                    button.targetGraphic.color = Color.white;
                }
                else
                {
                    button.targetGraphic.color = Color.green;
                }
            }

            // TODO : 추후 최적화 필요
            //Button getButton = musicPrefab.GetComponent<Button>();
            //getButton = infiniteScroll.Target.GetComponent<Button>();
            //getButton.targetGraphic.color = Color.white;
        }
    }
}