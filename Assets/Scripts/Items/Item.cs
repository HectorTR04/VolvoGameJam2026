using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    BaseItem baseData;

    public Item(BaseItem baseData)
    {
        this.baseData = baseData;
    }

    public void OnInteract()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
