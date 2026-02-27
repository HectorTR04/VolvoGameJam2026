using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera m_playerCam;

    private PlayerActions m_actions;
    private PlayerActions.MovementActions m_movementActions;
    private PlayerActions.InteractionActions m_interactions;
    private PlayerMovement m_playerMovement;
    private PlayerAnimator m_playerAnimator;
    private CharacterController m_characterController;

    public Camera PlayerCamera { get { return m_playerCam; } }

    #region Unity Methods
    private void OnEnable()
    {
        m_movementActions.Enable();
        m_interactions.Enable();
    }
    private void OnDisable()
    {
        m_movementActions.Disable();
        m_interactions.Disable();
    }
    private void Awake()
    {
        m_actions = new PlayerActions();
        m_movementActions = m_actions.Movement;
        m_interactions = m_actions.Interaction;
        m_interactions.Interact.performed += ctx => m_playerAnimator.AnimateInteraction();

    }
    private void Start()
    {
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerAnimator = GetComponent<PlayerAnimator>();
        m_characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        m_playerMovement.Move(m_movementActions.Walk.ReadValue<Vector2>(), m_characterController);
        m_playerAnimator.Animate(m_movementActions.Walk.ReadValue<Vector2>());
    }
    #endregion
}
