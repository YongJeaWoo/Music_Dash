using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator animator = null;

    private E_AniState aniState = E_AniState.Run;
    public E_AniState AniState => aniState;

    private E_AttackState attackState = E_AttackState.Attack1;
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

    public void AttackRandomPlay()
    {
        int ranValue = Random.Range(0, (int)E_AttackState.Length);
        E_AttackState newState = (E_AttackState)ranValue;

        if (attackState != newState)
        {
            Speed = 1f;
            attackState = newState;
            animator.Play(attackState.ToString(), 0, 0f);
        }
    }
}
