using Unity.VisualScripting;
using UnityEngine;

public class ItemPedestal : MonoBehaviour
{
    [SerializeField] BaseItem storedItem;
    [SerializeField] BaseRecipe recipe;
    [SerializeField] GameObject itemObject;

    private void Start()
    {
        itemObject.SetActive(false);
    }

    private void Update()
    {
        IsDiscovered();
    }

    public void IsDiscovered()
    {
        if (storedItem == null) return;
        if(storedItem.discovered)
        {
            itemObject.SetActive(true);
        }
    }

}
