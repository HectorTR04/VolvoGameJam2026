using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public float EnergyLevel;

    void Start()
    {
        EnergyLevel = 100f; // Initialize energy level to 100
    }
    void Update()
    {

    }

    public void IncreaseEnergy(float amount)
    {
        EnergyLevel += amount;
    }

    public void DecreaseEnergy(float amount)
    {
        EnergyLevel -= amount;
    }
}
