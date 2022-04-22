using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    public int health;
    public string currentLevel;
    public int playerLevel;
    public PlayerData()
    {
        health = 10;
        playerLevel = 0;
    }
    public void LoseHealth(int amount)
    {
        if (health > 0)
        {
            health -= amount;
        }
    }
}