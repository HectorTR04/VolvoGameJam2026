using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class IncineratorScript : MachineBase
{
    [SerializeField] private ParticleSystem incineratorParticles;

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (isOn)
            {
                TurnOff();
            }
            else
            {
                TurnOn();
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (!isOn || energyManager == null) return;

        if (other.GetComponent<Item>())
        {
            Item tempItem = other.GetComponent<Item>();
            energyManager.AddEnergy(tempItem.baseData.energyValue); //Increase energy by baseItem's energy value
            Destroy(other.gameObject);
        }
    }
    protected override void OnTurnedOn()
    {
        if (incineratorParticles)
        {
            incineratorParticles.Play();
            energyManager.SpendEnergy(1f);
        }
    }
    protected override void OnTurnedOff()
    {
        if(incineratorParticles) incineratorParticles.Stop();
    }
}
