using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private AnimationController animationController;

    [Tooltip("Player Info")]
    private int currentHp;
    private Vector2 playerPos = new Vector2(-14, -5.5f);

    [Tooltip("Player Behaviour Check")]
    private bool isJump, isTop = false;
    private bool isFall = false;
    private bool isMoving = true;
    private bool isDamaged = false;
    private bool isDead = false;

    [Tooltip("Player Info Property")]
    public int CurrentHp
    {
        get => currentHp;
        set => currentHp = value;
    }
    public bool Immute => PlayerInfo.IMMUTE_TIME > 0f;

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

        if (!isMoving && Input.GetButtonDown("KeyDown") && !isFall && !isJump) animationController.AttackRandomPlay();

        if (!isMoving && Input.GetButtonDown("KeyUp"))
        {
            isJump = true;
            isFall = false;
            animationController.AnimationPlay(E_AniState.Jump);
        }

        else if (transform.position.y <= playerPos.y)
        {
            isJump = false;
            isTop = false;
            isFall = false;
            transform.position = playerPos;
            animationController.AnimationPlay(E_AniState.Run);
        }

        if (isJump)
        {
            if (transform.position.y <= PlayerInfo.JUMP_POWER - 0.1f && !isTop && !isFall)
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, PlayerInfo.JUMP_POWER),
                    PlayerInfo.GRAVITY_POWER * Time.deltaTime);
            }

            else
            {
                isTop = true;
            }

            if (transform.position.y > playerPos.y && isTop)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPos, PlayerInfo.GRAVITY_POWER * Time.deltaTime);
                animationController.AnimationPlay(E_AniState.Fall);
            }

            if (transform.position.y > playerPos.y) InstrusionLanding();
        }
    }

    private void InstrusionLanding()
    {
        if (currentHp <= 0f) return;

        if (Input.GetButtonDown("KeyDown"))
        {
            isFall = true;
        }

        if (isFall)
        {
            transform.position = Vector2.Lerp(transform.position,
                new Vector2(transform.position.x, PlayerInfo.LANDING), PlayerInfo.LANDING_POWER * Time.deltaTime);
        }
    }

    public void JumpAttack()
    {
        transform.position = new Vector2(transform.position.x, PlayerInfo.JUMP_POWER);

        animationController.AttackRandomPlay();
    }

    private void Dead()
    {
        animationController.AnimationPlay(E_AniState.Dead);
        isDead = true;
        GameManager.Instance.ChangeGameOverState();
    }

    private IEnumerator InvincibleTime()
    {
        int countTime = 0;
        isDamaged = true;

        while (countTime < 20)
        {
            if (countTime % 2 == 0) spriteRenderer.color = new Color32(255, 255, 255, 90);
            else spriteRenderer.color = new Color32(255, 255, 255, 180);

            yield return new WaitForSeconds(0.1f);

            countTime++;
        }

        spriteRenderer.color = new Color32(255, 255, 255, 255);

        isDamaged = false;
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note") && !isDamaged && !isDead)
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
