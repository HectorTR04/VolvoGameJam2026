using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    [SerializeField] private MoneyManager MoneyManager;
    [SerializeField] private PlayerInteraction PlayerInteraction;

    public void OnInteract()
    {
        if (PlayerInteraction == null) return;
        if(MoneyManager == null) return;
        if (PlayerInteraction.GetItem() == null) return;

        SellItem();
    }

    public void SellItem()
    {
        MoneyManager.IncreaseMoney(PlayerInteraction.GetItem().baseData.sellValue);
        PlayerInteraction.GetRidOfItem();
    }

}
