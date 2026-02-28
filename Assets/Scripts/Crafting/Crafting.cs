using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public List<BaseRecipe> recipes;
    public BaseItem[] inputs = new BaseItem[2];

    public void Update()
    {
        CheckCraftingOutput(inputs);
    }

    public void CheckCraftingOutput(BaseItem[] inputItems)
    {
        foreach(BaseRecipe recipe in recipes)
        {
            if(DoesRecipeExist(inputItems, recipe))
            {
                Debug.Log("recipe exists lol");
                OutputItem(recipe);
            }
        }
    }

    public bool DoesRecipeExist(BaseItem[] inputItems, BaseRecipe recipe)
    {
        if (CheckIfInputsMatchRecipe(inputItems, recipe)) return true;

        Debug.Log("Flipping");

        BaseItem[] flippedInputs = FlipInputs(inputItems);

        if (CheckIfInputsMatchRecipe(flippedInputs, recipe)) return true;

        Debug.Log("Flipped recipe aint it");

        return false;
    }

    public BaseItem[] FlipInputs(BaseItem[] inputItems)
    {
        BaseItem[] temp = new BaseItem[inputItems.Length];
        for (int i = inputItems.Length - 1; i >= 0; i--)
        {
            temp[(temp.Length - 1) - i] = inputItems[i];
            //Debug.Log("element " + i + " " + temp[i]);
        }
        return temp;
    }

    public bool CheckIfInputsMatchRecipe(BaseItem[] inputItems, BaseRecipe recipe)
    {
        for (int i = 0; i < recipe.recipe.Length; i++)
        {
                Debug.Log("Recipe " + i + " is: " + recipe.recipe[i]);
                Debug.Log("Input " + i + " is: " + inputItems[i]);
                if (recipe.recipe[i] != inputItems[i])
                {
                    Debug.Log(recipe.recipe[i] + " does not " + inputItems[i]);                
                    return false;
                }
            
        }
        return true;
    }

    public void OutputItem(BaseRecipe recipe)
    {
        //do something with recipe.outputItem
    }

}
