using System.Collections;
using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    private float panelMoveTime = 0.5f;
    private bool playAnimation = false;

    private Vector3 showPosition;
    private Vector3 hidePosition;

    private RectTransform rect;

    private void OnEnable()
    {
        rect = GetComponent<RectTransform>();

        hidePosition = new Vector3(0, 1080, 0);
        showPosition = new Vector3(0, 0, 0);
    }

    public void InitializeResultPanel()
    {
        playAnimation = true;
        StartCoroutine(MovePanel());
    }

    public void OnExitResultPanel()
    {
        if (playAnimation) return;

        PopupManager.Instance.RemovePopUp<ResultPanel>();
    }

    private IEnumerator MovePanel()
    {
        Vector3 startPosition = showPosition;
        Vector3 endPosition = hidePosition;

        float startTime = Time.time;
        float endTime = startTime + panelMoveTime;

        while (Time.time < endTime)
        {
            float tempTime = (Time.time - startTime) / panelMoveTime;

            rect.anchoredPosition = Vector3.Lerp(showPosition, endPosition, tempTime);

            yield return null;
        }

        rect.anchoredPosition = endPosition;

        playAnimation = false;
    }
}
