using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private float m_rotationSpeed;

    private readonly float gravity = -9.8f;
    private Vector3 velocity;

    public void Move(Vector2 moveInput, CharacterController controller)
    {
        Vector3 movement = new(moveInput.x, 0, moveInput.y);
        controller.Move(m_speed * Time.deltaTime * movement);
        velocity.y += gravity * Time.deltaTime;
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }
        if (movement != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, m_rotationSpeed * Time.deltaTime);
        }
        controller.Move(velocity * Time.deltaTime);
    }
}
