using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Potion,
        UnitSpawner
    }

    public ItemType itemType;
    public int id;
    public string name;
    public string description;
    public Sprite sprite;
    public int amount;
}
