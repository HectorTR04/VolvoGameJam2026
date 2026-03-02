using Unity.VisualScripting;
using UnityEngine;

public class ItemPedestal : MonoBehaviour
{
    [SerializeField] BaseItem storedItem;
    [SerializeField] BaseRecipe recipe;
    [SerializeField] GameObject itemObject;
    public bool active;

    private void Start()
    {
        itemObject = GetComponentInChildren<Item>().gameObject;
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
            active = true;
        }
    }

}
