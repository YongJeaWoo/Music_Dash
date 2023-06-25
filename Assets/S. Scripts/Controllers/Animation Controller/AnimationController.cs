using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator animator = null;

    private E_JudgeState judgeState = E_JudgeState.None;
    private E_AniState aniState = E_AniState.Run;
    private E_AttackState attackState = E_AttackState.None;

    public float Speed
    {
        get => animator.speed;
        set => animator.speed = value;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void VerdictAnimationPlay(E_Judge _verdict)
    {
        Speed = 1f;
        animator.Play(_verdict.ToString(), 0, 0);
    }

    public void JudgeAnimationPlay(E_JudgeState _state)
    {
        if (judgeState != _state)
        {
            Speed = 1f;
            judgeState = _state;
            animator.Play(judgeState.ToString(), 0, 0);
        }
    }

    public void AnimationPlay(E_AniState _state)
    {
        if (aniState != _state)
        {
            Speed = 1f;
            aniState = _state;
            animator.Play(aniState.ToString(), 0, 0);
        }
    }
    
    public void AnimationDirectPlay(E_AniState _state)
    {
        Speed = 1f;
        animator.Play(aniState.ToString(), 0, 0);
    }

    public void AttackRandomPlay()
    {         
        int ranValue = Random.Range(1, (int)E_AttackState.Length);
        E_AttackState newState = (E_AttackState)ranValue;
        
        Speed = 1f;
        attackState = newState;
        animator.Play(attackState.ToString(), 0, 0f);
        StartCoroutine(nameof(AfterAttack));
    }

    private IEnumerator AfterAttack()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        AnimationDirectPlay(E_AniState.Run);
    }
}
