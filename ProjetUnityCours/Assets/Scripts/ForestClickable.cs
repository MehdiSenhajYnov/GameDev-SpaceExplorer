using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestClickable : MonoBehaviour, ClickableObject
{

    static CutButton cutButton;
    public static CutButton GetCutButton
    {   get
        {
            if (cutButton == null)
            {
                cutButton = FindObjectOfType<CutButton>(true);
            }
            return cutButton;
        }
    }
    
    public Cost cutCost = new Cost(3, 0);
    
    public void OnOtherObjectClicked(GameObject otherClicked)
    {
        BuyInfo.Instance.HideBuyInfo();
        SelectionFrame.Instance.Desactivate();
        if (otherClicked != GetCutButton.gameObject)
        {
            GetCutButton.DesactiveButton();
        }
    }

    public void OnSpriteClicked()
    {
        SelectionFrame.Instance.SetAt(transform.position);
        GetCutButton.ActiveButton(this);
        BuyInfo.Instance.ShowBuyInfo(cutCost);
    }

    public void OnSpriteClicking()
    {

    }

    public void OnSpriteUnClicked()
    {
    }
    public GameObject obj
    {
        get
        {
            return gameObject;
        }
    }

}
