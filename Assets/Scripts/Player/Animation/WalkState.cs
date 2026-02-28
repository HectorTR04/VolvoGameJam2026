using UnityEngine;

public class WalkState : AnimationState
{
    public WalkState(Animator playerAnimator)
    {
        m_playerAnimator = playerAnimator;
    }
    public override void Enter()
    {
        m_playerAnimator.SetBool("isWalking", true);
    }
    public override void Exit()
    {
        m_playerAnimator.SetBool("isWalking", false);
    }
}
