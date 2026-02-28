using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class IncineratorScript : MachineBase
{
    [SerializeField] private ParticleSystem incineratorParticles;
    [SerializeField] private float energyPerTrash = 10f;
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
    // public void TurnOnIncinerator()
    // {
    //     if (isOn || energyManager == null) return;
    //     if (energyManager.energyLevel <= 0f) return; // optional: don't allow turning on at 0

    //     if (incineratorParticles) incineratorParticles.Play();
    //     isOn = true;
    // }
    // public void TurnOffIncinerator()
    // {
    //     if (!isOn) return;

    //     if (incineratorParticles) incineratorParticles.Stop();
    //     isOn = false;
    // }
    public void OnTriggerEnter(Collider other)
    {
        if (!isOn || energyManager == null) return;

        if (other.CompareTag("Trash"))
        {
            other.gameObject.SetActive(false);
            energyManager.AddEnergy(energyPerTrash);
        }
    }
    protected override void OnTurnedOn()
    {
        if(incineratorParticles) incineratorParticles.Play();
    }
    protected override void OnTurnedOff()
    {
        if(incineratorParticles) incineratorParticles.Stop();
    }
}
