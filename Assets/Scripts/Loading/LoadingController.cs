using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    private static string nextScene;

    [SerializeField]
    private Slider progressBar;

    private float speed = 0.4f;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene(sceneName);
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;

            if (progressBar.value < 1f) progressBar.value = Mathf.MoveTowards(progressBar.value, 1f, Time.deltaTime * speed);
            else if (progressBar.value >= 1f) op.allowSceneActivation = true;
        }
    }
}
