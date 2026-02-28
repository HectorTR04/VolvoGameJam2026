using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    public BaseItem baseData;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public BaseItem GetBase()
        { return baseData; }
}
