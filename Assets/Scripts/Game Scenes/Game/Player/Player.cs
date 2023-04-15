using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private AnimationController animationController;

    [Tooltip("Player Info")]
    private float currentHp;
    private Vector2 playerPos = new Vector2(-14, -5.5f);

    [Tooltip("Player Behaviour Check")]
    private bool isJump, isTop = false;
    private bool isFall = false;
    private bool isAttacked;
    private bool isMoving = true;

    [Tooltip("Player Info Property")]
    public float CurrentHp { get => currentHp; set => currentHp = value; }
    public bool Immute => PlayerInfo.IMMUTE_TIME > 0f;

    private void Awake()
    {
        InitPlayer();
    }

    private void Update()
    {
        JumpAction();
        GroundAction();
        PlayerDead();
    }

    private void InitPlayer()
    {
        transform.position = new Vector2(-22, -5.5f);

        currentHp = PlayerInfo.PLAYER_MAXHP;
        isAttacked = false;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animationController = GetComponentInChildren<AnimationController>();

        StartCoroutine(MovePlayer(new Vector2(-14, -5.5f), 1.5f));
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

    private void GroundAction()
    {
        if (currentHp <= 0f) return;

        if (!isMoving && Input.GetButtonDown("KeyDown") && !isFall)
        {
            animationController.AttackRandomPlay();
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
            animationController.AnimationPlay(E_AniState.Fall);
        }
    }

    private void JumpAction()
    {
        if (currentHp <= 0f) return;

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

            if (transform.position.y > playerPos.y)
            {
                InstrusionLanding();
            }
        }
    }

    public void Attacked()
    {
        isAttacked = true;
        animationController.AnimationPlay(E_AniState.Damage);
        StartCoroutine(InvincibleTime());
    }

    private IEnumerator InvincibleTime()
    {
        int countTime = 0;

        while (countTime < 8)
        {
            if (countTime % 2 == 0) spriteRenderer.color = new Color32(255, 255, 255, 90);
            else spriteRenderer.color = new Color32(255, 255, 255, 180);

            yield return new WaitForEndOfFrame();

            countTime++;
        }

        spriteRenderer.color = new Color32(255, 255, 255, 255);

        isAttacked = false;

        yield return null;
    }

    private void PlayerDead()
    {
        if (currentHp <= 0f)
        {
            currentHp = 0f;
            animationController.AnimationPlay(E_AniState.Dead);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note") && !isAttacked)
        {
            Attacked();
            currentHp -= 20f;
        }
    }
}
