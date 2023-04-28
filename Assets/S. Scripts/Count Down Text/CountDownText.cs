using System.Collections;
using UnityEngine;
using TMPro;

public class CountDownText : MonoBehaviour
{
    private bool isAnimating = false;

    private TextMeshProUGUI readyText;
    private Animator readyAnimator;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (isAnimating) return;

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
        var timer = new WaitForSeconds(2.0f);

        readyText.gameObject.SetActive(true);
        readyAnimator.SetBool(AnimatorName.READY_NAME, true);
        readyText.text = "Ready ...";
        yield return timer;

        readyText.text = "Go !";

        readyAnimator.SetBool(AnimatorName.READY_NAME, false);
        isAnimating = true;
        yield return new WaitForSeconds(readyAnimator.GetCurrentAnimatorStateInfo(0).length);

        isAnimating = false;
        readyText.gameObject.SetActive(false);

        yield return null;
    }
}
