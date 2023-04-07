using UnityEngine;
using UnityEngine.UIElements;

public enum E_BackGround
{
    Desert,
    Graveyard,
    Mountain,
    Snow
}

public class RandomBackGroundController : MonoBehaviour
{
    private const string PATH_DESERT = "InGame/BackGrounds/Desert";
    private const string PATH_GRAVEYARD = "InGame/BackGrounds/Graveyard";
    private const string PATH_MOUNTAIN = "InGame/BackGrounds/Mountain";
    private const string PATH_SNOW = "InGame/BackGrounds/Snow";

    [SerializeField]
    private SpriteRenderer[] currentObj, nextObj;

    private Sprite[] sprites;

    E_BackGround randomBackGround;

    private void Start()
    {
        SetRandomBG();
    }

    private void SetRandomBG()
    {
        int count = System.Enum.GetValues(typeof(E_BackGround)).Length;
        randomBackGround = (E_BackGround)Random.Range(0, count);

        switch (randomBackGround)
        {
            case E_BackGround.Desert:
                {
                    sprites = Resources.LoadAll<Sprite>(PATH_DESERT);
                }
                break;
            case E_BackGround.Graveyard:
                {
                    sprites = Resources.LoadAll<Sprite>(PATH_GRAVEYARD);
                }
                break;
            case E_BackGround.Mountain:
                {
                    sprites = Resources.LoadAll<Sprite>(PATH_MOUNTAIN);
                }
                break;
            case E_BackGround.Snow:
                {
                    sprites = Resources.LoadAll<Sprite>(PATH_SNOW);
                }
                break;
        }

        for (int i = 0; i < currentObj.Length; i++)
        {
            currentObj[i].sprite = sprites[i];
        }

        for (int j = 0; j < nextObj.Length; j++)
        {
            nextObj[j].sprite = sprites[j];
        }
    }
}
