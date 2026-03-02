using UnityEngine;
using UnityEngine.InputSystem;

public class PowerButton : MonoBehaviour
{
    [SerializeField] private MachineBase targetMachine;
    [SerializeField] private string playerTag = "Player";

    private bool playerInRange;

    private void Reset()
    {
        // Helpful defaults when adding the component
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }
        private void Update()
    {
        if (!playerInRange) return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Press();
        }
    }

    public void Press()
    {
        if (targetMachine == null)
        {
            Debug.LogError($"{name}: No target machine assigned.", this);
            return;
        }

        targetMachine.Toggle();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
            playerInRange = false;
    }
}
