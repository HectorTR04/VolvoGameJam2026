using System.Net;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator m_animator;
    private AnimationState currentState;
    private bool m_isInteracting;

    public AnimationState CurrentState { get { return currentState; } }

    #region Unity Methods
    void Start()
    {
        m_animator = GetComponent<Animator>();
        currentState = new IdleState(m_animator);
        currentState.Enter();
    }
    #endregion

    public void Animate(Vector2 movement)
    {
        if (currentState is InteractState)
        {
            AnimatorStateInfo stateInfo = m_animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.normalizedTime >= 1f)
            {
                m_isInteracting = false;
            }
        }
        if (movement != Vector2.zero && currentState is not WalkState && !m_isInteracting)
        {
            TransitionToNewState(new WalkState(m_animator));
        }
        if(movement == Vector2.zero && currentState is not IdleState && !m_isInteracting)
        {
            TransitionToNewState(new IdleState(m_animator));
        }
    }

    public void AnimateInteraction()
    {
        m_isInteracting = true;
        TransitionToNewState(new InteractState(m_animator));
    }
    private void TransitionToNewState(AnimationState nextState)
    {
        currentState.Exit();

        currentState = nextState;
        currentState.Enter();
    }
}
