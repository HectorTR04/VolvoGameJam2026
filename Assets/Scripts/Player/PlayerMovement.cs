using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_speed;

    public void Move(Vector2 moveInput, CharacterController controller)
    {
        Vector3 moveDirection = new(moveInput.x, 0, moveInput.y);
        controller.Move(m_speed * Time.deltaTime * moveDirection);      
    }
}
