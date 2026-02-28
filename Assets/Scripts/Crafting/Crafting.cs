using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public List<BaseRecipe> recipes;
    public Item[] inputs = new Item[2];
    public BaseItem[] baseItemInputs = new BaseItem[2];

    public void Awake()
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            baseItemInputs[i] = inputs[i].GetBase();
            if (baseItemInputs[i] == null) Debug.Log("baseitem " + i);
            if (inputs[i] == null) Debug.Log("inputs " + i);
        }
        CheckCraftingOutput(baseItemInputs);
    }

    public void Update()
    {
    }

    public void CheckCraftingOutput(BaseItem[] inputItems)
    {
        foreach(BaseRecipe recipe in recipes)
        {
            if(DoesRecipeExist(inputItems, recipe))
            {
                Debug.Log("recipe exists lol");
                Craft(recipe);
            }
        }
    }

    public bool DoesRecipeExist(BaseItem[] inputItems, BaseRecipe recipe)
    {
        if (CheckIfInputsMatchRecipe(inputItems, recipe)) return true;

        BaseItem[] flippedInputs = FlipInputs(inputItems);

        if (CheckIfInputsMatchRecipe(flippedInputs, recipe)) return true;

        return false;
    }

    public BaseItem[] FlipInputs(BaseItem[] inputItems)
    {
        BaseItem[] temp = new BaseItem[inputItems.Length];
        for (int i = inputItems.Length - 1; i >= 0; i--)
        {
            temp[(temp.Length - 1) - i] = inputItems[i];
        }
        return temp;
    }

    public bool CheckIfInputsMatchRecipe(BaseItem[] inputItems, BaseRecipe recipe)
    {
        for (int i = 0; i < recipe.recipe.Length; i++)
        {
            if (recipe.recipe[i] != inputItems[i])
            {
                return false;
            }
        }
        return true;
    }

    public virtual void Craft(BaseRecipe recipe)
    {
        for(int i = 0; i < inputs.Length; i++)
        {
            baseItemInputs[i] = null;
            Destroy(inputs[i].gameObject);
            inputs[i] = null;
        }

        Item outputItem = new Item();
        outputItem.baseData = recipe.outputItem;

        //instantiate prefab with the crafted item

    }

}
