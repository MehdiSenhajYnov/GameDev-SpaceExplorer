using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuScripts : MonoBehaviour
{
    public Button PlayButton;
    public Button QuitButton;

    void Start()
    {
        PlayButton.onClick.AddListener(Play);
        QuitButton.onClick.AddListener(Quit);
    }

    void Play()
    {
        SceneManager.LoadScene("Game");
    }

    void Quit()
    {
        Application.Quit();
    }
}
