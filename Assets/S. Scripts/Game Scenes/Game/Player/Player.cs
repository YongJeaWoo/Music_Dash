using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private AnimationController animationController;

    private int currentHp;
    private Vector2 playerPos = new Vector2(-14, -5.5f);

    private bool isMoving = false;
    private bool isJumping = false;
    private bool isFalling = false;
    private bool isDamaged = false;
    private bool isDead = false;

    private bool isKeyDownPressed = false;
    private bool isKeyUpPressed = false;

    public int CurrentHp
    {
        get => currentHp;
        set => currentHp = value;
    }

    private void Update()
    {
        InputAction();
    }

    public void InitializePlayer()
    {
        transform.position = new Vector2(-22f, -5.5f);

        currentHp = PlayerInfo.PLAYER_MAXHP;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animationController = GetComponentInChildren<AnimationController>();

        StartCoroutine(MovePlayer(new Vector2(-14, -5.5f), 1.8f));
    }

    private IEnumerator MovePlayer(Vector2 targetPosition, float duration)
    {
        isMoving = true;
        float elapsedTime = 0f;
        Vector2 startingPosition = transform.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime * 0.5f;
            transform.position = Vector2.Lerp(startingPosition, targetPosition, elapsedTime);
            yield return null;
        }

        transform.position = targetPosition;

        isMoving = false;
    }

    private void InputAction()
    {
        if (currentHp <= 0f) return;

        if (!isMoving)
        {
            if (Input.GetButtonDown("KeyDown") && !isFalling && !isJumping)
            {
                animationController.AttackRandomPlay();
                isKeyDownPressed = true;
            }

            if (Input.GetButtonDown("KeyUp"))
            {
                if (!isJumping && !isFalling)
                {
                    isJumping = true;
                    animationController.AnimationPlay(E_AniState.Jump);
                }
                else if (isJumping && !isFalling)
                {
                    isFalling = true;
                    animationController.AnimationPlay(E_AniState.Fall);
                }

                isKeyUpPressed = true;
            }

            if (isKeyDownPressed && isKeyUpPressed)
            {
                if (transform.position.y > PlayerInfo.MIDDLE_JUMP)
                {
                    transform.position = new Vector2(transform.position.x, PlayerInfo.MIDDLE_JUMP);
                }
                else
                {
                    transform.position = playerPos;
                }

                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, PlayerInfo.MIDDLE_JUMP),
                    PlayerInfo.GRAVITY_POWER * Time.deltaTime);

                animationController.AttackRandomPlay();

                isKeyDownPressed = false;
                isKeyUpPressed = false;
            }

            if (!isJumping && !isFalling && transform.position.y <= playerPos.y)
            {
                animationController.AnimationPlay(E_AniState.Run);
            }

            if (isJumping)
            {
                if (transform.position.y <= PlayerInfo.JUMP_POWER - 0.1f && !isFalling)
                {
                    transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, PlayerInfo.JUMP_POWER),
                        PlayerInfo.GRAVITY_POWER * Time.deltaTime);
                }
                else
                {
                    isJumping = false;
                    isFalling = true;
                }
            }

            if (isFalling)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPos, PlayerInfo.GRAVITY_POWER * Time.deltaTime);

                if (transform.position.y <= playerPos.y)
                {
                    isFalling = false;
                    animationController.AnimationPlay(E_AniState.Run);
                }
            }
        }
    }

    private void Dead()
    {
        animationController.AnimationPlay(E_AniState.Dead);
        GameManager.Instance.ChangeGameOverState();
    }

    private IEnumerator InvincibleTime()
    {
        int countTime = 0;

        while (countTime < 20)
        {
            if (countTime % 2 == 0) spriteRenderer.color = new Color32(255, 255, 255, 90);
            else spriteRenderer.color = new Color32(255, 255, 255, 180);

            yield return new WaitForSeconds(0.1f);

            countTime++;
        }

        spriteRenderer.color = new Color32(255, 255, 255, 255);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note") && !isDead)
        {
            if (currentHp > 0 && !isJumping && !isFalling)
            {
                isDamaged = true;
                CurrentHp -= 10;
                UIManager.Instance.comboBackground.SetActive(false);
                ScoreManager.Instance.SetComboZero();
                animationController.AnimationPlay(E_AniState.Damage);
                StartCoroutine(InvincibleTime());

                if (CurrentHp <= 0)
                {
                    CurrentHp = 0;
                    Dead();
                }
            }
        }
    }
}
