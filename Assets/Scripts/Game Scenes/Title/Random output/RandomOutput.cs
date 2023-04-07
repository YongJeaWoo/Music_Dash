using UnityEngine;
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
        AudioManager.Instance.RandomMusicPlay();
    }

    private void RandomImage()
    {
        int index = Random.Range(0, imageSprites.Length);
        Sprite selectSprite = imageSprites[index];
        image.sprite = selectSprite;
    }
}
