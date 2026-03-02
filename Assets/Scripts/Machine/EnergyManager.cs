using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    [SerializeField] private float maxEnergy = 100f;
    public float EnergyLevel { get; private set; }
    private readonly List<MachineBase> activeMachines = new List<MachineBase>();

    [Header("Battery Production")]
    [SerializeField] private float batteryBaseRate = 0.75f;
    [SerializeField] private float batteryExponent = 1.05f;
        private int batteryCount;

    public void RegisterBattery(BatteryItem battery) => batteryCount++;
    public void UnregisterBattery(BatteryItem battery) => batteryCount = Mathf.Max(0, batteryCount - 1);


    void Awake()
    {
        EnergyLevel = maxEnergy; // Initialize energy level to maxEnergy
    }
    void Update()
    {
        Debug.Log($"batteries={batteryCount} prod/s={BatteryProductionPerSecond():F2} energy={EnergyLevel:F1}");
                AddEnergy(BatteryProductionPerSecond() * Time.deltaTime);
        if (activeMachines.Count == 0) return;
        
        float totalDrainPerSecond = 0f;

        for (int i = activeMachines.Count - 1; i >= 0; i--)
        {
            if (activeMachines[i] == null)
            {
                activeMachines.RemoveAt(i);
                continue;
            }

            totalDrainPerSecond += activeMachines[i].DrainPerSecond;
        }
        
                SpendEnergy(totalDrainPerSecond * Time.deltaTime);
                        if (EnergyLevel <= 0f)
        {
            EnergyLevel = 0f;

            for (int i = activeMachines.Count - 1; i >= 0; i--)
            {
                if (activeMachines[i] != null)
                    activeMachines[i].ForcedOffDueToNoEnergy();
            }

            activeMachines.Clear();
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
        return EnergyLevel >= amount;
    }
    public void AddEnergy(float amount)
    {
        EnergyLevel = Mathf.Clamp(EnergyLevel + amount, 0f, maxEnergy);
    }
    public void SpendEnergy(float amount)
    {
        EnergyLevel = Mathf.Clamp(EnergyLevel - amount, 0f, maxEnergy);
    }
    private float BatteryProductionPerSecond()
    {
        if (batteryCount <= 0) return 0f;
        return batteryBaseRate * Mathf.Pow(batteryCount, batteryExponent);
    }
}
