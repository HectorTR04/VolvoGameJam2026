using UnityEngine;

public class BaseItem : MonoBehaviour
{
    string itemName;
    float energyValue;
    bool discovered;
    Sprite sprite;

}

public struct ItemRecipe
{
    public BaseItem item1;
    public BaseItem item2;
    bool discovered;
}
