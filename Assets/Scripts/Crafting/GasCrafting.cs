using System.Collections;
using UnityEngine;

public class GasCrafting : Crafting
{
    public override void Craft(BaseRecipe recipe)
    {
        WaitForGas();
        Debug.Log(WaitForGas());
    }

    IEnumerator WaitForGas()
    {
        yield return new WaitForSeconds(1000);
    }
}
