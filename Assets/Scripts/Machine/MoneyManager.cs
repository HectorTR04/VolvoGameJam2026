using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public float CurrentMoney;
    void Start()
    {
        CurrentMoney = 0f;
    }
    public void IncreaseMoney(float amount)
    {
        CurrentMoney += amount;
    }

    public void DecreaseMoney(float amount)
    {
        CurrentMoney -= amount;
    }
}
