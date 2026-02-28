using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Vector3 m_verticalOffset = new(0, 0.5f, 0);
    private GameObject m_heldItem;
    private readonly float m_interactionRange = 2f;

    #region Unity Methods
    private void Update()
    {
        
    }
    #endregion

    public void Interact()
    {
        if (Physics.Raycast(transform.position + m_verticalOffset, transform.forward, out RaycastHit hit, m_interactionRange))
        {
            Debug.DrawRay(transform.position + m_verticalOffset, transform.forward * hit.distance, Color.red);
            if(hit.collider == null ) { return; }
            if(hit.collider.gameObject.TryGetComponent(out IInteractable detectedInteraction))
            {
                detectedInteraction.OnInteract();
            }
        }
    }
}
