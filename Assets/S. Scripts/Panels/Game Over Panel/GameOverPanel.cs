using System.Collections;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    private float panelMoveTime = 0.5f;
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

        PopupManager.Instance.RemovePopUp<GameOverPanel>();
    }

    private IEnumerator MovePanel(bool _isShow)
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
    }
}
