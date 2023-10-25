using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoinVisual : Singleton<CoinVisual>
{
    [SerializeField] private TMP_Text text;
    private TMP_Text GetText
    {
        get
        {
            if (text == null)
            {
                text = GetComponent<TMP_Text>();
            }
            return text;
        }
    }



    private void Start()
    {
        RessourceManager.Instance.coinsChanged += UpdateText;
        UpdateText(RessourceManager.Instance.Coins, 0, 0);
    }

    public void UpdateText(int newValue, int oldValue, int amountChanged)
    {
        GetText.text = newValue.ToString();
    }


}
