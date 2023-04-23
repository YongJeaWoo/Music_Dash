using System.Collections;
using UnityEngine;
using TMPro;

public class CountDownText : MonoBehaviour
{
    public static bool isCounting = false;

    private TextMeshProUGUI readyText;
    private Animator readyAnimator;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        CheckCountMusic();
    }

    private void Init()
    {
        readyText = GetComponent<TextMeshProUGUI>();
        readyAnimator = GetComponent<Animator>();
    }

    private void CheckCountMusic()
    {
        if (AudioManager.Instance.audioSource.clip.name.ToString() == "Start" && AudioManager.Instance.audioSource.time >= 2.2f)
        {
            StartCoroutine(CountDown());
        }
    }

    private IEnumerator CountDown()
    {
        var readyTimer = new WaitForSeconds(2.0f);
        var aniTimer = new WaitForSeconds(2.0f);

        readyText.gameObject.SetActive(true);
        readyAnimator.SetBool(AnimatorName.READY_NAME, true);
        readyText.text = "Ready ...";
        yield return readyTimer;

        readyText.text = "Go !";
        yield return new WaitForEndOfFrame();
        readyAnimator.SetBool(AnimatorName.READY_NAME, false);
        yield return aniTimer;
        isCounting = false;
        readyText.gameObject.SetActive(false);

        yield return null;
    }
}
