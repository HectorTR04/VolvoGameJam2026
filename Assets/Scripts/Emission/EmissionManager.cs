using UnityEngine;

public class EmissionManager : MonoBehaviour
{
    [SerializeField] private float m_emissionDecrease = 0.01f;

    public readonly float MaximumEmissions = 100f;

    private readonly float m_timeBetweenUpdates = 1f;
    private float m_updateTimer;
    private float m_currentEmission;
    public float CurrentEmission { get { return m_currentEmission; } }

    void Start()
    {
        m_currentEmission = 0f;
    }

    void Update()
    {
        if (m_currentEmission <= 0f) return;
        m_updateTimer += Time.deltaTime;
        if (m_updateTimer > m_timeBetweenUpdates)
        {
            m_currentEmission -= m_emissionDecrease;
            m_updateTimer = 0f;
        }
    }

    public void IncreaseEmissions(float increase)
    {
        m_currentEmission += increase;
    }
}
