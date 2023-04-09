using Unity.VisualScripting;
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

        private Button currentSelectedButton;
        public Button CurrentSelectedButton
        {
            get
            {
                return currentSelectedButton;
            }

            set
            {
                currentSelectedButton = value;
            }
        }

        private InfiniteScroll infiniteScroll;

        public InfoController info;

        private void Start()
        {
            SetInit();
        }

        private void Update()
        {
            InputKeys();
        }

        private void SetInit()
        {
            infiniteScroll = GetComponent<InfiniteScroll>();

            if (musicPrefab == null) return;

            foreach(MusicData musicData in MusicDataManager.Instance.GetMusicDataList())
            {
                var obj = Instantiate(musicPrefab);
                obj.transform.SetParent(contentTransform);

                MusicSelectEntity musicSelectEntity = obj.GetComponent<MusicSelectEntity>();
                if (musicSelectEntity != null) musicSelectEntity.Initialize(musicData);
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
            if (currentSelectedButton != null) currentSelectedButton.targetGraphic.color = Color.green;

            infiniteScroll.Previous();
            SetPoint();
        }

        private void ScrollDown()
        {
            if (currentSelectedButton != null) currentSelectedButton.targetGraphic.color = Color.green;

            infiniteScroll.Next();
            SetPoint();
        }

        private void SetPoint()
        {
            Button targetButton = infiniteScroll.Target.GetComponent<Button>();
            targetButton.targetGraphic.color = Color.white;
            currentSelectedButton = targetButton;
            info.OnSelect();
        }
    }
}