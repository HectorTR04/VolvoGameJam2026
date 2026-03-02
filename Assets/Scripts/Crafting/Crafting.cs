using System.Collections.Generic;
using Assets.Scripts.AudioSystem;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

public class Crafting : MonoBehaviour
{
    public List<BaseRecipe> recipes;
    public Item[] inputs = new Item[2];
    public BaseItem[] baseItemInputs = new BaseItem[2];

    [SerializeField] EmissionManager EmissionManager;
    [SerializeField] EnergyManager EnergyManager;
    [SerializeField] PlayerInteraction PlayerInteraction;
    [SerializeField] GameObject[] prefabs = new GameObject[8];

    public void Awake()
    {   
        //CheckCraftingOutput();
    }

    public void Update()
    {
    }

    public void CheckCraftingOutput()
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            baseItemInputs[i] = inputs[i].GetBase();
            if (baseItemInputs[i] == null) Debug.Log("baseitem " + i);
            if (inputs[i] == null) Debug.Log("inputs " + i);
        }

        foreach (BaseRecipe recipe in recipes)
        {
            if(DoesRecipeExist(baseItemInputs, recipe))
            {
                Debug.Log("recipe exists lol");
                Craft(recipe);
                DestroyInputs();
                return;
            }
        }

        DestroyInputs();
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
        SoundManager.PlayAt(SoundType.SFX_CraftingDone, transform.position);

        Item outputItem = new Item();
        outputItem.baseData = recipe.outputItem;
        if (outputItem.baseData.discovered == false)
        {
            outputItem.baseData.discovered = true;
        }

        foreach (GameObject prefab in prefabs)
        {
            if (prefab.GetComponent<Item>() == outputItem)
            {
                GameObject instantiatedItem = Instantiate(prefab);
                PlayerInteraction.PickUpItem(instantiatedItem);
            }
        }
       

    }

    public void DestroyInputs()
    {
        for (int i = 0; i < 2; i++)
        {
            Debug.Log(inputs[i].baseData.name);
            //Energy cost is deducted here
            //EmissionManager.IncreaseEmissions(inputs[i].baseData.emissionValue); //increase emissions;
            EnergyManager.SpendEnergy(inputs[i].baseData.energyCostValue); //Spend energy
            baseItemInputs[i] = null;
            Destroy(inputs[i].gameObject);
            inputs[i] = null;
        }
    }

}
