using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    public int health;
    public int money;
    public string currentLevel;
    public int playerLevel;
    public PlayerData()
    {
        health = 10;
        playerLevel = 0;
        money = 100;
    }
    public void LoseHealth(int amount)
    {
        if (health > 0)
        {
            health -= amount;
        }
        if (health <= 0)
        {
            LevelManager.RestartGame();
        }
    }

    public bool ConsumeMoney(int amount)
    {
        if (amount > money)
        {
            return false;
        }
        money -= amount;
        return true;
    }

    public void GainMoney(int amount)
    {
        money += amount;
    }
}