using UnityEngine;
using UnityEngine.Events;

public class CraftingTable : MonoBehaviour, IInteractable
{
    [SerializeField]
    PlayerInteraction PlayerInteraction;

    [SerializeField]
    Crafting Crafting;

    [SerializeField]
    bool isInteracting;

    public void Update()
    {
        //if(isInteracting)
        //{
        //    OnInteract();
        //    isInteracting = false; 
        //}
        //Test code don't worry about this
    }

    public void OnInteract()
    {
        if (PlayerInteraction == null) return;
        if(PlayerInteraction.GetItem() == null) return;

        PlaceItemOnTable();

        if (Crafting.inputs[1] != null) Crafting.CheckCraftingOutput();
            
    }

    public void PlaceItemOnTable()
    {
        Debug.Log(Crafting.inputs.Length);
        for(int i = 0; i <  Crafting.inputs.Length; i++)
        {
            if (Crafting.inputs[i] == null)
            {
                //add code for physically placing the items on the table
                Crafting.inputs[i] = PlayerInteraction.GetItem();
                PlayerInteraction.GetRidOfItem();
                break;
            }
        }
    }
}
