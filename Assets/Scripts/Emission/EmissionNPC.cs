using UnityEngine;

public class EmissionNPC : MonoBehaviour
{
    [SerializeField] private GameObject m_messageIndicator;
    [SerializeField] private GameObject m_message;
    [SerializeField] private GameObject m_emissionManagerObj;

    private EmissionManager m_emissionManager;

    #region Unity Methods
    private void Start()
    {
        m_emissionManager = m_emissionManagerObj.GetComponent<EmissionManager>();
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

    }

    private string MessageText()
    {
        float emissionAsPercentage = m_emissionManager.CurrentEmission / m_emissionManager.MaximumEmissions;
        if (emissionAsPercentage <= 0.2)
        {
            return "There has been a slight increase in emissions please be careful!";
        }
        if (emissionAsPercentage <= 0.5)
        {
            return "Rizz";
        }
        if (emissionAsPercentage <= 0.7)
        {
            return "Bombaclat";
        }
        return "Emissions are low, things are looking good!";
    }
}
