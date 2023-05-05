using System;
using System.Collections;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    private float panelMoveTime = 0.25f;
    private bool playAnimation = false;

    private Vector3 hidePosition;
    private Vector3 showPosition;

    private RectTransform rectTrans;

    private void OnEnable()
    {
        rectTrans = GetComponent<RectTransform>();

        hidePosition = new Vector3(1920, 0, 0);
        showPosition = new Vector3(0, 0, 0);
    }

    public void InitializeOverPanel()
    {
        playAnimation= true;
        StartCoroutine(MovePanel(true));
    }

    public void OnExitOverPanel()
    {
        if (playAnimation) return;

        playAnimation = true;

        StartCoroutine(MovePanel(true, () => PopupManager.Instance.RemovePopUp<GameOverPanel>()));
    }

    private IEnumerator MovePanel(bool _isShow, Action _onComplete = null)
    {
        Vector3 startPosition = _isShow ? hidePosition : showPosition;
        Vector3 endPosition = _isShow ? showPosition : hidePosition;

        float startTime = Time.time;
        float endTime = startTime + panelMoveTime;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / panelMoveTime;

            rectTrans.anchoredPosition = Vector3.Lerp(startPosition, endPosition, t);

            yield return null;
        }

        rectTrans.anchoredPosition = endPosition;

        playAnimation = false;

        _onComplete?.Invoke();
    }


    //private Vector3 hidePosition = new Vector3(1920, 0, 0);
    //private Vector3 showPosition = new Vector3(0, 0, 0);

    //private float moveDuration = 1f;

    //private RectTransform rectTransform;

    //private void Start()
    //{
    //    Init_Basic();
    //}

    //private void Init_Basic()
    //{
    //    rectTransform = GetComponent<RectTransform>();
    //    rectTransform.anchoredPosition = hidePosition;
    //}

    //private IEnumerator Move(Vector3 targetPosition, System.Action onFinished = null)
    //{
    //    Vector3 startPosition = rectTransform.anchoredPosition;
    //    float elapsedTime = 0f;
    //    while (elapsedTime < moveDuration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        float t = Mathf.Clamp01(elapsedTime / moveDuration);
    //        rectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);
    //        yield return null;
    //    }
    //    rectTransform.anchoredPosition = targetPosition;
    //    onFinished?.Invoke();
    //}

    //public void Show()
    //{
    //    gameObject.SetActive(true);
    //    StopAllCoroutines();
    //    StartCoroutine(Move(showPosition));
    //}

    //public void Hide()
    //{
    //    StopAllCoroutines();
    //    StartCoroutine(Move(hidePosition, () => gameObject.SetActive(false)));
    //}
}
