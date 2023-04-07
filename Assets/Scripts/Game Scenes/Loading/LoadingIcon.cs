using UnityEngine;
using UnityEngine.UI;

public class LoadingIcon : MonoBehaviour
{
    private float time;
    private float fadeTime = 1f;

    private int rotateSpeed = -50;

    private void Update()
    {
        RotateIcon();
        FadeEffect();
    }

    private void RotateIcon()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    private void FadeEffect()
    {
        time += Time.deltaTime;

        if (time < fadeTime) GetComponent<Image>().color = new Color(1, 1, 1, 1f - time / fadeTime);
        else
        {
            time = 0;
            GetComponent<Image>().color = new Color(1, 1, 1, 0f - time / fadeTime);
        }
    }
}
