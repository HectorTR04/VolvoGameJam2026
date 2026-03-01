using UnityEngine;

public class PowerButton : MonoBehaviour, IInteractable
{
    public Spawner spawnerOnOff;
    public bool isOn = true;

    void IInteractable.OnInteract()
    {
        if (spawnerOnOff == null)
            return;

        if (isOn)
        {
            spawnerOnOff.enabled = false;
            spawnerOnOff.isOn = false;

            Debug.Log("Off");
            isOn = false;
        }
        else
        {
            spawnerOnOff.enabled = true;
            spawnerOnOff.isOn = true;

            Debug.Log("On");
            isOn = true;
        }
    }
}
