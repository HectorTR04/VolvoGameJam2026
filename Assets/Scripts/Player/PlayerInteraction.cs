using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Vector3 m_verticalOffset = new(0, 0.5f, 0);
    private GameObject m_heldItem;
    private readonly Vector3 m_aboveHeadOffset = new(0, 2f, 0);
    private readonly float m_interactionRange = 2f;

    public void Interact()
    {
        if (Physics.Raycast(transform.position + m_verticalOffset, transform.forward, out RaycastHit hit, m_interactionRange))
        {
            Debug.DrawRay(transform.position + m_verticalOffset, transform.forward * hit.distance, Color.red);
            if (hit.collider.gameObject.TryGetComponent(out IInteractable detectedInteraction))
            {
                Debug.Log("Interaction Detected With: " + hit.collider.gameObject);
                detectedInteraction.OnInteract();
            }
            if (hit.collider.gameObject.TryGetComponent(out Item detectedItem))
            {
                Debug.Log("Item Detected: " + hit.collider.gameObject);
                HandleItemInteraction(detectedItem.gameObject);
                return;
            }        
        }
        if (m_heldItem != null)
        {
            DropHeldItem();
        }
    }

    private void HandleItemInteraction(GameObject detectedItem)
    {
        if(m_heldItem == null)
        {
            PickUpItem(detectedItem);
        }
        else
        {
            SwapHeldItem(detectedItem);
        }
    }

    private void DropHeldItem()
    {
        m_heldItem.transform.parent = null;
        m_heldItem.GetComponent<Collider>().enabled = true;
        m_heldItem.transform.position = transform.position + transform.forward * 1.5f;
        Debug.Log("Item: " + m_heldItem + "dropped.");
        m_heldItem = null;
    }

    private void PickUpItem(GameObject detectedItem)
    {
        m_heldItem = detectedItem;
        m_heldItem.transform.SetParent(gameObject.transform);
        m_heldItem.transform.position = gameObject.transform.position + m_aboveHeadOffset;
        m_heldItem.GetComponent<Collider>().enabled = false;
    }

    private void SwapHeldItem(GameObject detectedItem)
    {
        m_heldItem.transform.parent = null;
        m_heldItem.GetComponent<Collider>().enabled = true;
        m_heldItem.transform.position = transform.position + transform.forward * 1.5f;
        Debug.Log("Swapped Item: " + m_heldItem + "for: " + detectedItem);
        m_heldItem = detectedItem;
        m_heldItem.transform.SetParent(gameObject.transform);
        m_heldItem.transform.position = gameObject.transform.position + m_aboveHeadOffset;
        m_heldItem.GetComponent<Collider>().enabled = false;
    }
}
