using UnityEngine;

public class PowerButton : MonoBehaviour
{
    [SerializeField] private MachineBase targetMachine;

    public void Press()
    {
        if (targetMachine == null)
        {
            Debug.LogError($"{name}: No target machine assigned.", this);
            return;
        }

        targetMachine.Toggle();
    }
}
