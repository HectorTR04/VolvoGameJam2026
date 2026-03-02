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
    private float textTimer;
    private readonly float timeToResetText = 2f;

    private void Start()
    {
        itemObject.SetActive(false);
        recipeText = textObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        IsDiscovered();
        textTimer += Time.deltaTime;
        if (textTimer > timeToResetText)
        {
            recipeText.text = string.Empty;
            textTimer = 0f;
        }
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
        if (storedItem.discovered)
        {
            recipeText.text = $"{storedItem.itemName}: \n {recipe.recipe[0].itemName} + {recipe.recipe[1].itemName}";
        }
    }
}
