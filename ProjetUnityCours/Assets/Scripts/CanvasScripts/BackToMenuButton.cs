using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuButton : MonoBehaviour, ClickableObject
{
    public GameObject obj => gameObject;

    public void OnClick()
    {
    }

    public void OnOtherObjectClicked(GameObject otherClicked)
    {
    }

    public void OnSpriteClicked()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnSpriteClicking()
    {
    }

    public void OnSpriteUnClicked()
    {
    }
}
