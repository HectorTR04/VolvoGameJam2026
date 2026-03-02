using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Events;

public class CraftingTable : MonoBehaviour, IInteractable
{
    [SerializeField]
    PlayerInteraction PlayerInteraction;

    [SerializeField]
    Crafting Crafting;

    [SerializeField]
    bool isInteracting;

    [SerializeField]
    Transform anchorPoint;


    void Start()
    {
       // OnInteract();
    }
    public void Update()
    {
        if(isInteracting)
        {
            isInteracting = false; 
        }
        //Test code don't worry about this
    }

    public void OnInteract()
    {
        if (PlayerInteraction == null) return;
        if (PlayerInteraction.GetItem() == null)
        {
            Debug.Log("null");
                return;
        }

        PlaceItemOnTable();

        if (Crafting.inputs[1] != null) 
            Crafting.CheckCraftingOutput();

        if(Crafting.instantiatedItem)
        {
            PlaceItemUsingColliderBounds(Crafting.instantiatedItem);
            Crafting.instantiatedItem = null;
        }
            
    }

    public void PlaceItemOnTable()
    {
        for (int i = 0; i < Crafting.inputs.Length; i++)
        {
            if (Crafting.inputs[i] == null)
            {
                //add code for physically placing the items on the table
                Crafting.inputs[i] = PlayerInteraction.GetItem();
                PlayerInteraction.SetItemAsNull();
                //PlayerInteraction.DropHeldItem();
                PlaceItemUsingColliderBounds(Crafting.inputs[i].gameObject);
                //foreach(var collider in Crafting.inputs[i].GetComponents<SphereCollider>())
                //{
                //    if(collider.radius > 0.006)
                //    {
                //        Destroy(collider);
                //    }
                //}

                break;
            }
        }
    }

    public void PlaceItemUsingColliderBounds(GameObject item)
    {
        item.transform.SetParent(anchorPoint);
        Renderer renderer = item.GetComponentInChildren<Renderer>();

        if (renderer == null)
        {
            Debug.Log("No renderer");
            return;
        }

        Bounds bounds = renderer.bounds;

        float verticalOffset = bounds.extents.y + anchorPoint.GetComponent<BoxCollider>().bounds.extents.y;

        item.transform.localPosition = Vector3.zero;
        //item.transform.localPosition = new Vector3(0, verticalOffset, 0);

        item.transform.localRotation = Quaternion.identity;
    }
}
