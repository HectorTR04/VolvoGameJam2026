using System;
using Assets.Scripts.AudioSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class IncineratorScript : MachineBase
{
    [SerializeField] private ParticleSystem incineratorParticles;
    private EmissionManager emissionManager;
    IncineratorScript allIncinerators;
    protected override void Awake()
    {
        base.Awake();
        allIncinerators = FindAnyObjectByType<IncineratorScript>();
        emissionManager = FindAnyObjectByType<EmissionManager>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (!isOn || energyManager == null) return;

        if (other.GetComponent<Item>())
        {
            Item tempItem = other.GetComponent<Item>();
            energyManager.AddEnergy(tempItem.baseData.energyValue); //Increase energy by baseItem's energy value
            Destroy(other.gameObject);
            SoundManager.Play(SoundType.SFX_Burnsound);
            emissionManager.IncreaseEmissions(other.GetComponent<Item>().baseData.emissionValue); //Increase emissions by baseItem's emission value
        }
    }
    protected override void OnTurnedOn()
    {
        if (incineratorParticles)
        {
            incineratorParticles.Play();
            energyManager.SpendEnergy(1f);
            SoundManager.PlayAt(SoundType.SFX_IncineratorOn, transform.position);
        }
    }
    protected override void OnTurnedOff()
    {
        if(incineratorParticles) incineratorParticles.Stop();
        SoundManager.StopSfx(SoundType.SFX_IncineratorOn);
    }
}
