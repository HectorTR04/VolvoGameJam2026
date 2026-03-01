using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField]
    public BaseItem baseData;


    public void OnInteract()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public BaseItem GetBase()
        { return baseData; }
}
