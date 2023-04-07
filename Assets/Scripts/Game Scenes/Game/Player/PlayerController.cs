using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [Tooltip("Player Info")]
    private float immuteTime = 2.5f;
    private float maxHp = 100f;
    private float currentHp;
    private Vector2 playerPos = new Vector2(-14, -5.5f);

    [Tooltip("Player Behaviour Check")]
    private int randomAttack = 2;

    private bool isJump, isTop = false;
    private bool isLanding = false;
    private bool isAttacked = false;

    private WaitForSeconds waitForSeconds = new WaitForSeconds(0.2f);

    [Tooltip("Player Info Property")]
    public float MaxHp { get => maxHp; }
    public float CurrentHp { get => currentHp; }
    public bool Immute => immuteTime > 0f;

    [Header("Player Behaviour")]
    [SerializeField]
    private float gravity;
    [Space(5f)]
    [SerializeField]
    private float jumpPower;
    [Space(5f)]
    [SerializeField]
    private float landing;
    [Space(5f)]
    [SerializeField]
    private float landingPower;

    private Animator playerAnimator;

    private void Awake()
    {
        InitPlayer();
    }

    private void Start()
    {

    }

    private void Update()
    {
        JumpAction();
        GroundAction();
        PlayerDead();
    }

    private void InitPlayer()
    {
        currentHp = maxHp;
        isAttacked = false;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    private void GroundAction()
    {
        if (currentHp <= 0f) return;

        if (Input.GetButtonDown("KeyDown") && !isLanding)
        {
            int attackIndex = Random.Range(0, randomAttack + 1);
            playerAnimator.SetTrigger("Attack" + attackIndex);
        }
    }

    private void InstrusionLanding()
    {
        if (currentHp <= 0f) return;

        if (Input.GetButtonDown("KeyDown")) isLanding = true;

        if (isLanding)
        {
            transform.position = Vector2.Lerp(transform.position,
                new Vector2(transform.position.x, landing), landingPower * Time.deltaTime);
        }
    }

    private void JumpAction()
    {
        if (currentHp <= 0f) return;

        if (Input.GetButtonDown("KeyUp"))
        {
            isJump = true;
            isLanding = false;
        }

        else if (transform.position.y <= playerPos.y)
        {
            isJump = false;
            isTop = false;
            isLanding = false;
            transform.position = playerPos;
        }

        if (isJump)
        {
            if (transform.position.y <= jumpPower - 0.1f && !isTop && !isLanding)
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, jumpPower),
                    gravity * Time.deltaTime);

                playerAnimator.SetBool("Jump", true);
            }

            else
            {
                isTop = true;
            }

            if (transform.position.y > playerPos.y && isTop)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPos, gravity * Time.deltaTime);
                playerAnimator.SetBool("Jump", false);
                playerAnimator.SetTrigger("Fall");
                playerAnimator.SetFloat("Fall Speed", 0.25f);
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
        StartCoroutine(InvincibleTime());
    }

    private IEnumerator InvincibleTime()
    {
        playerAnimator.SetTrigger("Damage");

        int countTime = 0;

        while (countTime < 8)
        {
            if (countTime % 2 == 0) spriteRenderer.color = new Color32(255, 255, 255, 90);
            else spriteRenderer.color = new Color32(255, 255, 255, 180);

            yield return waitForSeconds;

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
            playerAnimator.ResetTrigger("Damage");
            playerAnimator.SetTrigger("Dead");
        }
    }
}
