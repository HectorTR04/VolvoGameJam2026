using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPedestal : MonoBehaviour, IInteractable
{
    [SerializeField] BaseItem storedItem;
    [SerializeField] BaseRecipe recipe;
    [SerializeField] GameObject itemObject;
    [SerializeField] GameObject textObject;
    public bool active;

    private TextMeshProUGUI recipeText;


    private void Start()
    {
        itemObject.SetActive(false);
        recipeText = textObject.GetComponent<TextMeshProUGUI>();
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

    public void OnInteract()
    {
        recipeText.text = $"{storedItem.name}: \n {recipe.recipe[0].name} + {recipe.recipe[1].name}";
    }
}
