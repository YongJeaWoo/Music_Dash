using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        GameManager.Instance.CurrentState = E_GameState.Init;
    }
}
