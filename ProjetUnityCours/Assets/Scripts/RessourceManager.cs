using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RessourceManager : Singleton<RessourceManager>
{
    public int MaxCoins = 1000;
    public int Coins;

    public int MaxWood = 1000;
    public int Wood;

    public event Action<int, int, int, int> woodChanged;
    public event Action<int, int, int, int> coinsChanged;

    private void Start()
    {
        Coins = 0;
        AddCoins(100);

        Wood = 0;
        AddWood(100);
    }

    public void ChangeMaxCoins(int newMaxCoins)
    {
        MaxCoins = newMaxCoins;
        coinsChanged?.Invoke(Coins, Coins, 0, MaxCoins);

    }

    public void AddCoins(int amount)
    {
        int tempCoins = Coins;
        Coins += amount;
        if (Coins >= MaxCoins)
        {
            Coins = MaxCoins;
        }
        coinsChanged?.Invoke(Coins, tempCoins, amount, MaxCoins);

    }

    public void RemoveCoins(int amount)
    {
        coinsChanged?.Invoke(Coins - amount, Coins, -amount, MaxCoins);
        Coins -= amount;
        if (Coins <= 0)
        {
            Coins = 0;
            Debug.Log("Game Over");
        }
    }

    public void ChangeMaxWood(int newMaxWood)
    {
        MaxWood = newMaxWood;
        woodChanged?.Invoke(Wood, Wood, 0, MaxWood);
    }

    public void AddWood(int amount)
    {
        int tempWood = Wood;
        Wood += amount;
        if (Wood >= MaxWood)
        {
            Wood = MaxWood;
        }
        woodChanged?.Invoke(Wood, tempWood, amount, MaxWood);
    }

    public void RemoveWood(int amount)
    {
        woodChanged?.Invoke(Wood - amount, Wood, -amount, MaxWood);
        Wood -= amount;
    }

    public bool CanBuy(Cost cost)
    {
        return Coins >= cost.Coin && Wood >= cost.Wood;
    }

    public void AddRessource(Cost cost)
    {
        AddCoins(cost.Coin);
        AddWood(cost.Wood);
    }


    
    public void BuyBuild(Building building, Cost buildCost)
    {
        if (CanBuy(buildCost))
        {
            Buy(buildCost);
            GameManager.Instance.buildingsBuyed.Add(building);
        }
    }
    public void Buy(Cost buildCost)
    {

        RemoveCoins(buildCost.Coin);
        RemoveWood(buildCost.Wood);
    }
}
