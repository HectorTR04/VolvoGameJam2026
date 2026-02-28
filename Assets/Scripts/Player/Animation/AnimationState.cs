using UnityEngine;

public abstract class AnimationState
{
    protected Animator m_playerAnimator;

    public abstract void Enter();

    public abstract void Exit();
}
