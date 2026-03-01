using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class BaseItem : ScriptableObject
{
    public string itemName;
    public float energyValue;
    public float energyCostValue;
    public float emissionValue;
    public bool discovered;
    //public Sprite sprite;
    //public ItemType type;
}

//Lowk dont need the enum
public enum ItemType
{
    None,
    Water,
    Fire,
    Earth,
    Air,
    Steam,
    Dirt
}