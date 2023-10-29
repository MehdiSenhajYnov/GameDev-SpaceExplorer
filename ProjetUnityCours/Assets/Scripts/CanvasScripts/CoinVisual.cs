using UnityEngine;
using TMPro;
public class CoinVisual : MonoBehaviour
{
    private TMP_Text text;
    [SerializeField] private TMP_Text MaxCoinstext;
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
        UpdateText(RessourceManager.Instance.Coins, 0, 0, RessourceManager.Instance.MaxCoins);
    }

    public void UpdateText(int newValue, int oldValue, int amountChanged, int max)
    {
        GetText.text = newValue.ToString();
        MaxCoinstext.text = "MAX:"+max.ToString();
    }


}
