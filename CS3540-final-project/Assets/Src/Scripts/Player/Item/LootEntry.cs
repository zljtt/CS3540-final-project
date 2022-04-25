using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

[Serializable]
public class LootEntry
{
    public string loot;
    public int amountMin;
    public int amountMax;
    public float chance;

    public LootEntry(string item, int min, int max, float c)
    {
        loot = item;
        amountMax = max;
        amountMin = min;
        chance = c;
    }

    public LootEntry(Item item, int amount)
    {
        loot = item.GetRegistryName();
        amountMax = amount;
        amountMin = amount;
        chance = 1;
    }

    public void GenerateLoot(Vector3 position, int lootCount, float bonusChance)
    {

        for (int i = 0; i < lootCount; i++)
        {
            float c = UnityEngine.Random.Range(0.0f, 1.0f);
            Debug.Log("Loot " + loot + " for the " + i + "th time with chance " + c + "/" + chance * (bonusChance + 1));
            if (c < chance * (bonusChance + 1))
            {
                if (loot == "coin5")
                {
                    Debug.Log("Loot success and spawn a coin5");
                    GameObject itemWorld = GameObject.Instantiate(Resources.Load("Prefabs/Coin/Coin5"), position - Vector3.down * 1, Quaternion.Euler(0, 0, 0)) as GameObject;
                }
                else if (loot == "coin10")
                {
                    Debug.Log("Loot success and spawn a coin10");
                    GameObject itemWorld = GameObject.Instantiate(Resources.Load("Prefabs/Coin/Coin10"), position - Vector3.down * 1, Quaternion.Euler(0, 0, 0)) as GameObject;
                }
                else
                {
                    int amount = UnityEngine.Random.Range(amountMin, amountMax + 1);
                    Debug.Log("Loot success and spawn " + amount + " " + loot);
                    ItemStack item = new ItemStack(ItemDatabase.GetFromRegistryName(loot), amount);
                    GameObject itemWorld = ItemWorld.InstantiateWorldItem(item, position - Vector3.down * 1);
                }
            }
        }

    }
}