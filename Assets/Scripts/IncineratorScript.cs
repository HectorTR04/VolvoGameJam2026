using UnityEngine;
using UnityEngine.InputSystem;

public class IncineratorScript : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem incineratorParticles;

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            incineratorParticles.Play();
        }
    }
}
