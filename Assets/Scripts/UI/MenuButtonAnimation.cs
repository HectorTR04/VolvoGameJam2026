using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonAnimation : MonoBehaviour
{
    [SerializeField] Sprite m_defaultButtonSprite;
    [SerializeField] Sprite m_hoveredButtonSprite;
    [SerializeField] private Color m_hoverColor;

    private Image m_buttonImg;
    private TextMeshProUGUI m_buttonText;

    #region Unity Methods
    private void Start()
    {
        m_buttonImg = GetComponent<Image>();
        m_buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }
    #endregion

    public void CursorEnter()
    {
        m_buttonImg.sprite = m_hoveredButtonSprite;
        m_buttonText.color = Color.white;
    }

    public void CursorExit()
    {
        m_buttonImg.sprite = m_defaultButtonSprite;
        m_buttonText.color = m_hoverColor;
    }
}
