using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class IncineratorScript : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem incineratorParticles;
    [SerializeField]
    EnergyManager energyManager;
    private bool isOn;
    void Update()
    {
        if (isOn)
        {
            energyManager.energyLevel -= Time.deltaTime * 1f; // Decrease energy level over time when incinerator is on
            if (energyManager.energyLevel <= 0)
            {
                energyManager.energyLevel = 0;
                TurnOffIncinerator();
            }
        }
        Debug.Log($"Energy Level: {energyManager.energyLevel}");
    }
    public void TurnOnIncinerator()
    {
        if (!isOn)
        {
            incineratorParticles.Play();
            isOn = true;
        }
    }
    public void TurnOffIncinerator()
    {
        if (isOn)
        {
            incineratorParticles.Stop();
            isOn = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trash"))
        {
            other.gameObject.SetActive(false);
            energyManager.energyLevel += 10f; // Increase energy level when trash is incinerated
        }
    }

}
