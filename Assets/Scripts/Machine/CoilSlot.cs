using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class CoilSlot : MonoBehaviour, IInteractable
{

    [Header("References")]
    [SerializeField] private EnergyManager energyManager;
    [SerializeField] private PlayerInteraction player;

    [Header("Battery rules")]
    [SerializeField] private string batteryItemName = "Aluminium Battery";
    [SerializeField] private int capacity = 4;

    [Header("Snap points (optional but recommended)")]
    [SerializeField] private Transform[] snapPoints; // set size to 4 in inspector

    private readonly List<Item> storedItems = new();
    private readonly List<BatteryItem> storedBatteries = new();

    private void Awake()
    {
        if (!energyManager)
            energyManager = FindAnyObjectByType<EnergyManager>();
        if (!player)
            player = FindAnyObjectByType<PlayerInteraction>();

        if (!energyManager)
            Debug.LogError($"{name}: EnergyManager not found.", this);
        if (!player)
            Debug.LogError($"{name}: PlayerInteraction not found.", this);

        if (snapPoints != null && snapPoints.Length > 0 && snapPoints.Length < capacity)
            Debug.LogWarning($"{name}: snapPoints has fewer than capacity. Batteries may overlap.", this);
    }

    public void OnInteract()
    {
        if (!player || !energyManager) return;

        // If full, do nothing (or you can implement removal on second press)
        if (storedItems.Count >= capacity)
        {
            Debug.Log("Coil is full.");
            return;
        }

        Item held = player.GetItem();
        if (held == null || held.baseData == null)
        {
            Debug.Log("Player not holding an item.");
            return;
        }

        if (held.baseData.itemName != batteryItemName)
        {
            Debug.Log($"Not a battery: {held.baseData.itemName}");
            return;
        }

        BatteryItem batteryMarker = held.GetComponent<BatteryItem>();
        if (!batteryMarker)
        {
            Debug.Log("Held item missing BatteryItem component.");
            return;
        }

        InsertBattery(held, batteryMarker);
        player.GetRidOfItem();
    }

    private void InsertBattery(Item item, BatteryItem battery)
    {
        storedItems.Add(item);
        storedBatteries.Add(battery);

        item.transform.SetParent(null);

        // Snap to position
        Transform snap = GetSnapPointForIndex(storedItems.Count - 1);
        if (snap != null)
            item.transform.SetPositionAndRotation(snap.position, snap.rotation);

        // Collider back on (optional)
        var col = item.GetComponent<Collider>();
        if (col) col.enabled = true;

        // Lock physics
        if (item.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // Count this battery as active generation
        energyManager.RegisterBattery(battery);

        Debug.Log($"Inserted battery ({storedItems.Count}/{capacity})");
    }

    private Transform GetSnapPointForIndex(int index)
    {
        if (snapPoints == null || snapPoints.Length == 0) return null;
        if (index < 0 || index >= snapPoints.Length) return snapPoints[snapPoints.Length - 1];
        return snapPoints[index];
    }
}
