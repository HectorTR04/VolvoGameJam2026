using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    [SerializeField] private float maxEnergy = 100f;
    public float Energy { get; private set; }
    private readonly List<MachineBase> activeMachines = new List<MachineBase>();
    void Awake()
    {
        Energy = maxEnergy; // Initialize energy level to maxEnergy
    }
    void Update()
    {
        if(activeMachines.Count == 0) return;
        float totalDrainPerSecond = 0f;
        for(int i = activeMachines.Count - 1; i >= 0; i--)
        {
            if(activeMachines[i] == null)
            {
                activeMachines.RemoveAt(i);
                continue;
            }
            totalDrainPerSecond += activeMachines[i].DrainPerSecond;
        }
    }
    public void Register(MachineBase machine)
    {
        if (activeMachines == null)
        {
            return;
        }
        if(!activeMachines.Contains(machine))
        {
            activeMachines.Add(machine);
        }
    }
    public void Unregister(MachineBase machine)
    {
        if(activeMachines == null)
        {
            return;
        }
        activeMachines.Remove(machine);
    }
    public bool HasEnergy(float amount)
    {
        return Energy >= amount;
    }
    public void AddEnergy(float amount)
    {
        Energy = Mathf.Clamp(Energy + amount, 0f, maxEnergy);
    }
    public void SpendEnergy(float amount)
    {
        Energy = Mathf.Clamp(Energy - amount, 0f, maxEnergy);
    }

    public void IncreaseEnergy(float amount)
    {
        energyLevel += amount;
    }

    public void DecreaseEnergy(float amount)
    {
        energyLevel -= amount;
    }
}
