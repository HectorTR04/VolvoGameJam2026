using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public float energyLevel;
    void Start()
    {
        energyLevel = 100f; // Initialize energy level to 100
    }
    void Update()
    {

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
