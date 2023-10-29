using UnityEngine;
using TMPro;
public class WoodVisual : MonoBehaviour
{
    private TMP_Text text;
    [SerializeField] private TMP_Text MaxWoodtext;
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
        RessourceManager.Instance.woodChanged += UpdateText;
        UpdateText(RessourceManager.Instance.Wood, 0, 0, RessourceManager.Instance.MaxWood);
    }

    public void UpdateText(int newValue, int oldValue, int amountChanged, int max)
    {
        GetText.text = newValue.ToString();
        MaxWoodtext.text = "MAX:"+max.ToString();
    }


}
