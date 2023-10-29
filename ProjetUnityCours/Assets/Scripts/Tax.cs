using Unity.VisualScripting;
using UnityEngine;
using TMPro;
public class Tax : MonoBehaviour
{
    public float buildingsTaxRate = 1;
    public float dayTaxRate = 1;
    int taxAmount;

    int buildingsCount => GameManager.Instance.buildingsBuyed.Count;
    int day => DateSystem.Instance.day;

    public TMP_Text taxText;

    void Start()
    {
        DateSystem.Instance.OnDayChange += HaveToPayTax;
        DateSystem.Instance.OnDayChange += CalculateTax;
    }
    public void CalculateTax()
    {
        taxAmount = (int)(buildingsCount * buildingsTaxRate + day * dayTaxRate);
        taxText.text = taxAmount.ToString();
    }

    public void HaveToPayTax()
    {
        if (DateSystem.Instance.day % 10 == 0)
        {
            PayTax();
        }
    }

    public void PayTax()
    {
        if (RessourceManager.Instance.Coins >= taxAmount)
        {
            RessourceManager.Instance.Buy(new Cost(taxAmount, 0));
        }
        else
        {
            GameManager.Instance.GameOver();
        }
    }

}
