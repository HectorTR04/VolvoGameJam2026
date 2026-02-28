using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/CraftingRecipe", order = 2)]
public class BaseRecipe : ScriptableObject
{
    public BaseItem outputItem;
    public BaseItem[] recipe = new BaseItem[2];
}

