using UnityEngine;

namespace DTT.InfiniteScroll
{
    public class ScrollController : MonoBehaviour
    {
        [SerializeField]
        private GameObject musicPrefab;

        [SerializeField]
        private Transform contentTransform;

        private MusicSelectEntity musicSelectEntity = null;

        private MusicSelectEntity currentSelectedEntity;
        public MusicSelectEntity CurrentSelectedEntity
        {
            get
            {
                return currentSelectedEntity;
            }

            set
            {
                currentSelectedEntity = value;
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

            var musicDataList = MusicDataManager.Instance.GetMusicDataList();

            if (musicDataList == null || musicDataList.Count == 0) return;

            foreach (MusicData musicData in musicDataList)
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
            if (currentSelectedEntity != null) currentSelectedEntity.OnSelect(false);

            infiniteScroll.Previous();
            SetPoint();
        }

        private void ScrollDown()
        {
            if (currentSelectedEntity != null) currentSelectedEntity.OnSelect(false);

            infiniteScroll.Next();
            SetPoint();
        }

        private void SetPoint()
        {
            musicSelectEntity = infiniteScroll.Target.GetComponentInChildren<MusicSelectEntity>();
            musicSelectEntity.OnSelect(true);
            currentSelectedEntity = musicSelectEntity;
            info.OnRefresh();
        }
    }
}