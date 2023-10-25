using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceManager : Singleton<RessourceManager>
{

    public int Coins;
    public int Wood;

    public event Action<int, int, int> woodChanged;
    public event Action<int, int, int> coinsChanged;

    private void Start()
    {
        Coins = 0;
        AddCoins(100);

        Wood = 0;
        AddWood(100);
    }

    public void AddCoins(int amount)
    {
        coinsChanged?.Invoke(Coins + amount, Coins, amount);
        Coins += amount;
    }

    public void RemoveCoins(int amount)
    {
        coinsChanged?.Invoke(Coins - amount, Coins, -amount);
        Coins -= amount;
        if (Coins <= 0)
        {
            Coins = 0;
            Debug.Log("Game Over");
        }
    }

    public void AddWood(int amount)
    {
        woodChanged?.Invoke(Wood + amount, Wood, amount);
        Wood += amount;
    }

    public void RemoveWood(int amount)
    {
        woodChanged?.Invoke(Wood - amount, Wood, -amount);
        Wood -= amount;
    }



}
