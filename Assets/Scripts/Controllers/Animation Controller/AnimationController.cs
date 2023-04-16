using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator animator = null;

    private E_AniState aniState = E_AniState.Run;
    public E_AniState AniState => aniState;

    private E_AttackState attackState = E_AttackState.None;
    public E_AttackState AttackState => attackState;

    public float Speed
    {
        get => animator.speed;
        set => animator.speed = value;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        StartCoroutine(AfterAttack());
    }

    private IEnumerator AfterAttack()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        AnimationDirectPlay(E_AniState.Run);
    }
}
