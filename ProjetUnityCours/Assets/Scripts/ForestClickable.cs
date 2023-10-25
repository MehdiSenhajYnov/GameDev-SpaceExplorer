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
    public void OnOtherObjectClicked(GameObject otherClicked)
    {
        if (otherClicked == GetCutButton.gameObject)
        {
            return;
        }
        GetCutButton.DesactiveButton();
        SelectionFrame.Instance.Desactivate();
    }

    public void OnSpriteClicked()
    {
        SelectionFrame.Instance.SetAt(transform.position);
        GetCutButton.ActiveButton(this);
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
