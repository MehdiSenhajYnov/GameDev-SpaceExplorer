using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BuyInfo : Singleton<BuyInfo>
{

    public GameObject BuyInfoPanel;
    public TMP_Text CoinCostText;
    public TMP_Text WoodCostText;

    void Start()
    {
        BuyInfoPanel.SetActive(false);
    }

    void Update()
    {
    }

    public void ShowBuyInfo(Cost cost)
    {
        BuyInfoPanel.SetActive(true);
        CoinCostText.text = cost.CoinCost.ToString();
        WoodCostText.text = cost.WoodCost.ToString();
    }

    public void HideBuyInfo()
    {
        BuyInfoPanel.SetActive(false);
    }
}
