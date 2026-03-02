using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject m_itemTextObj;
    [SerializeField] private GameObject m_energyTextObj;
    [SerializeField] private GameObject m_moneyTextObj;
    [SerializeField] private GameObject m_clipboardObj;

    private TextMeshProUGUI m_heldItemText;
    private TextMeshProUGUI m_energyText;
    private TextMeshProUGUI m_moneyText;

    private bool m_showClipboard;

    #region Unity Methods
    private void Start()
    {
        m_heldItemText = m_itemTextObj.GetComponent<TextMeshProUGUI>();
        m_energyText = m_energyTextObj.GetComponent<TextMeshProUGUI>();
        m_moneyText = m_moneyTextObj.GetComponent<TextMeshProUGUI>();
        m_showClipboard = true;
    }
    #endregion

    public void UpdateUI(Item heldItemData, float energyData, float moneyData)
    {
        if (m_showClipboard)
        {
            m_clipboardObj.SetActive(true);
        }
        else
        {
            m_clipboardObj.SetActive(false);
        }
        m_moneyText.text = moneyData.ToString() + "$";
        m_energyText.text = energyData.ToString("F1");
        if (heldItemData == null) { m_heldItemText.text = string.Empty; return; }
        m_heldItemText.text = heldItemData.baseData.name;      
    }

    public void ToggleClipboard()
    {
        m_showClipboard = !m_showClipboard;
    }
}
