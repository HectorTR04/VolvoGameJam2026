using UnityEngine;

public class TrophyPedestal : ItemPedestal
{
    [SerializeField] ItemPedestal[] itemPedestals;
    [SerializeField] GameObject trophy;

    private void Start()
    {
        trophy = GameObject.Find("Trophy");
        trophy.SetActive(false);
        itemPedestals = FindObjectsByType<ItemPedestal>(default);
        active = true;
    }

    private void Update()
    {
        ShowTrophy();
    }

    public bool ArePedestalsUnlocked()
    {
        foreach (var pedestal in itemPedestals)
        {
            if (!pedestal.active) return false;
        }

        return true;
    }

    public void ShowTrophy()
    {
        if (!ArePedestalsUnlocked()) return;
        trophy.SetActive(true);
    }

}
