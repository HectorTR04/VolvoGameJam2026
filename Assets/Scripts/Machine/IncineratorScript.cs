using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class IncineratorScript : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem incineratorParticles;
    [SerializeField]
    private EnergyManager energyManager;
    [SerializeField]
    private float drainPerSecond = 1f;
    [SerializeField]
    private float energyPerTrash = 10f;
    private bool isOn;
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (isOn)
            {
                TurnOffIncinerator();
            }
            else
            {
                TurnOnIncinerator();
            }
        }

        if (!isOn || energyManager == null)
        {
            return;
        }
        energyManager.EnergyLevel -= Time.deltaTime * drainPerSecond;
        if (energyManager.EnergyLevel <= 0)
        {
            energyManager.EnergyLevel = 0f;
            TurnOffIncinerator();
        }
        Debug.Log($"Energy Level: {energyManager.EnergyLevel}");
    }
    public void TurnOnIncinerator()
    {
        if (isOn || energyManager == null) return;
        if (energyManager.EnergyLevel <= 0f) return; // optional: don't allow turning on at 0

        if (incineratorParticles) incineratorParticles.Play();
        isOn = true;
    }
    public void TurnOffIncinerator()
    {
        if (!isOn) return;

        if (incineratorParticles) incineratorParticles.Stop();
        isOn = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (!isOn || energyManager == null) return;

        if (other.CompareTag("Trash"))
        {
            other.gameObject.SetActive(false);
            energyManager.EnergyLevel += energyPerTrash;
        }
    }

}
