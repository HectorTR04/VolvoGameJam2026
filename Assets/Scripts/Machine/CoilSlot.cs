using UnityEngine;


[RequireComponent(typeof(Collider))]
public class CoilSlot : MonoBehaviour
{
        [SerializeField] private Transform snapPoint;
    [SerializeField] private EnergyManager energyManager;

    [SerializeField] private string batteryItemName = "Aluminium Battery"; // match your BaseItem name

    private Item currentItem;

    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void Awake()
    {
        if (!energyManager)
            energyManager = FindAnyObjectByType<EnergyManager>();

        if (!snapPoint)
            Debug.LogError($"{name}: snapPoint not assigned.", this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentItem != null) return; // already occupied

        Item item = other.GetComponentInParent<Item>();
        if (item == null || item.baseData == null) return;

        if (item.baseData.itemName != batteryItemName) return;

        Insert(item);
    }

    private void Insert(Item item)
    {
        currentItem = item;

        // snap into place
        item.transform.SetPositionAndRotation(snapPoint.position, snapPoint.rotation);

        // optional lock
        if (item.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        energyManager?.RegisterBattery();
    }

    public void Remove()
    {
        if (currentItem == null) return;

        if (currentItem.TryGetComponent<Rigidbody>(out var rb))
            rb.isKinematic = false;

        currentItem = null;
        energyManager?.UnregisterBattery();
    }
}
