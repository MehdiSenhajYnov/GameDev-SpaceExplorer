using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : Singleton<UpgradeButton>, ClickableObject
{
    Action UpgradeAction;
    Button button;
    public GameObject obj => gameObject;
    bool isClickable = true;
    // Start is called before the first frame update
    void Start()
    {
        DeactiveButton();
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveButton(Action buildUpgrade)
    {
        UpgradeAction = buildUpgrade;
        gameObject.SetActive(true);
        SetClickable();

    }

    public void ActiveNonClickable(Action buildUpgrade)
    {
        UpgradeAction = buildUpgrade;
        gameObject.SetActive(true);
        SetNonClickable();
    }

    public void SetNonClickable()
    {
        button.interactable = false;
        isClickable = false;
    }

    public void SetClickable()
    {
        button.interactable = true;
        isClickable = true;
    }

    public void DeactiveButton()
    {
        UpgradeAction = null;
        gameObject.SetActive(false);
    }

    public void OnButtonClicked()
    {
        Debug.Log("UPGRADE!");
        UpgradeAction?.Invoke();
    }

    public void OnSpriteClicked()
    {
        if (!isClickable) return;
        OnButtonClicked();
    }

    public void OnSpriteClicking()
    {
    }

    public void OnSpriteUnClicked()
    {
    }

    public void OnOtherObjectClicked(GameObject otherClicked)
    {
    }
}
