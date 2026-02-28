using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    BaseItem baseData;

    public Item(BaseItem baseData)
    {
        this.baseData = baseData;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
