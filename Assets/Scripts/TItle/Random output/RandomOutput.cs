using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RandomOutput : MonoBehaviour
{
    [SerializeField]
    private Sprite[] imageSprites;

    [SerializeField]
    private Image image;

    private void OnEnable()
    {
        RandomMusic();
        RandomImage();
    }

    private void RandomMusic()
    {
        if (SceneManager.GetActiveScene().name == "Loading") return;

        AudioManager.Instance.PlayRandomMusic();
    }

    private void RandomImage()
    {
        int index = Random.Range(0, imageSprites.Length);
        Sprite selectSprite = imageSprites[index];
        image.sprite = selectSprite;
    }
}
