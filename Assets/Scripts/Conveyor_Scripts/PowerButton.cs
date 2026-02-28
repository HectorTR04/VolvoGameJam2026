using UnityEngine;

public class PowerButton : MonoBehaviour
{
    public GameObject objToTurnOnOrOff;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            objToTurnOnOrOff.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            objToTurnOnOrOff.SetActive(true);
        }
    }
}
