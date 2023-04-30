using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{ 
    private Vector3 hidePosition = new Vector3(1920, 0, 0);
    private Vector3 showPosition = new Vector3(0, 0, 0);

    private float moveDuration = 1f;

    private RectTransform rectTransform;

    private void Start()
    {
        Init_Basic();
    }

    private void Init_Basic()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = hidePosition;
        gameObject.SetActive(false);
    }

    private IEnumerator Move(Vector3 targetPosition, System.Action onFinished = null)
    {
        Vector3 startPosition = rectTransform.anchoredPosition;
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            rectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
        rectTransform.anchoredPosition = targetPosition;
        onFinished?.Invoke();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(Move(showPosition));

        if (GameManager.Instance.CurrentState == E_GameState.Result)
        {
            if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            if (Input.GetKeyDown(KeyCode.Escape)) LoadingController.LoadScene("Select");
        }
    }

    public void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(Move(hidePosition, () => gameObject.SetActive(false)));
    }
}
