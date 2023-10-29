using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FastForwardScript : MonoBehaviour, ClickableObject
{


    public GameObject obj => gameObject;
    Image image;
    Color clickedColor = new Color(0.50f, 0.33f, 0.75f);
    bool fastForward = false;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DateSystem.Instance.timeMultiplier = 20;
            fastForward = true;
        } else if (Input.GetKeyUp(KeyCode.Space))
        {
            DateSystem.Instance.timeMultiplier = 1;
            fastForward = false;
        }

        if (fastForward)
        {
            image.color = clickedColor;
        }
        else
        {
            image.color = Color.white;
        }
    }

    public void OnSpriteClicked()
    {
        DateSystem.Instance.timeMultiplier = 20;
        fastForward = true;
    }

    public void OnSpriteClicking()
    {
    }

    public void OnSpriteUnClicked()
    {
        DateSystem.Instance.timeMultiplier = 1;
        fastForward = false;
    }

    public void OnOtherObjectClicked(GameObject otherClicked)
    {
    }
}
