using UnityEngine;

public class InteractState : AnimationState
{
    public InteractState(Animator playerAnimator)
    {
        m_playerAnimator = playerAnimator;
    }

    public override void Enter()
    {
        m_playerAnimator.SetBool("isInteracting", true);
    }

    public override void Exit()
    {
        m_playerAnimator.SetBool("isInteracting", false);
    }
}
