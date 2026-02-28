using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    public Color oldColor = Color.white;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = Color.red;
    }

    private void OnTriggerExit(Collider other)
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = oldColor;
    }
}
