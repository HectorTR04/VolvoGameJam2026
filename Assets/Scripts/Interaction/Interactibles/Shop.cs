using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    MoneyManager MoneyManager;
    PlayerInteraction PlayerInteraction;

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
