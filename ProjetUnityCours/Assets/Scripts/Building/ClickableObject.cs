using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ClickableObject
{
    void OnSpriteClicked();
    void OnSpriteClicking();
    void OnSpriteUnClicked();
    void OnOtherObjectClicked(GameObject otherClicked);
    public GameObject obj { get;}
}
