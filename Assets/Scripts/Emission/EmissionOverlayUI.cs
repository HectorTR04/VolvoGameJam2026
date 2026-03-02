using UnityEngine;
using UnityEngine.UI;
public class EmissionOverlayUI : MonoBehaviour
{
    [SerializeField] private EmissionManager emissionManager;
    [SerializeField] private Image emissionFillImage;

//Alpha when emission is 0
    private float minAlpha = 0f;

    //alpha when emission is max
    private float maxAlpha = 0.5f;
    //Smoothing
    private float alphaChangeSpeed = 5f;

    private float currentAlpha;

    private void Reset()
    {
        emissionFillImage = GetComponentInChildren<Image>();
    }
    private void Awake()
    {
        if (emissionFillImage == null)
            emissionFillImage = GetComponent<Image>();

        // Start at current alpha
        if (emissionFillImage != null)
            currentAlpha = emissionFillImage.color.a;
    }
    private void Update()
    {
    if(emissionManager == null || emissionFillImage == null) return;

        float emissionPercent = Mathf.Clamp01(emissionManager.CurrentEmission / emissionManager.MaximumEmissions);
        float targetAlpha = Mathf.Lerp(minAlpha, maxAlpha, emissionPercent);

        // Smoothly interpolate current alpha towards target alpha
        currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, Time.deltaTime * alphaChangeSpeed);

        Color color = emissionFillImage.color;
        color.a = currentAlpha;
        emissionFillImage.color = color;
    }

}
