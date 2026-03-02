using Unity.VisualScripting;
using UnityEngine;

public abstract class MachineBase : MonoBehaviour
{
    [SerializeField] protected EnergyManager energyManager;
    [SerializeField] private float drainPerSecond = 1f;

    public float DrainPerSecond => isOn ? drainPerSecond : 0f;
    protected bool isOn;
    public bool IsOn => isOn;

    protected virtual void Awake()
    {
        if(energyManager == null)
        {
            energyManager = FindAnyObjectByType<EnergyManager>();
        }
    }
    public void TurnOn()
    {
        if(isOn) return;
        if(energyManager == null) return;
        if (!energyManager.HasEnergy(0.01f)) //If no energy, don't turn on
        {
            return;
        }
        isOn = true;
        energyManager.Register(this);
        OnTurnedOn();
    }
    public void TurnOff()
    {
        if(!isOn) return;
        if (energyManager != null)
        {    
            energyManager.Unregister(this);
        }
        isOn = false;
        OnTurnedOff();
    }
    public void Toggle()
{
    if (isOn) TurnOff();
    else TurnOn();
}
    public void ForcedOffDueToNoEnergy()
    {
        if (!isOn) return;

        if (energyManager != null)
            energyManager.Unregister(this);

        isOn = false;
        OnPowerLost();
    }
    protected abstract void OnTurnedOn();
    protected abstract void OnTurnedOff();
    protected virtual void OnPowerLost() => OnTurnedOff();
    protected virtual void OnDisable()
    {
        if(isOn && energyManager != null)
        {
            energyManager.Unregister(this);
        }
    }
}
