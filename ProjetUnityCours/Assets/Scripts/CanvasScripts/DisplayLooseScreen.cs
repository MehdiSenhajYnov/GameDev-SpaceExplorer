using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class DisplayLooseScreen : Singleton<DisplayLooseScreen>
{
    public GameObject LooseScreen;
    public Button PlayAgainButton;
    public Button BackToMenuButton;
    public TMP_Text ScoreInfo;
    public TMP_Text RecordInfo;
    public int dayRecord;


    void Start()
    {
        PlayAgainButton.onClick.AddListener(PlayAgain);
        BackToMenuButton.onClick.AddListener(BackToMenu);
    }

    public void ShowLooseScreen()
    {
        DisplayInfo();
        LooseScreen.SetActive(true);
    }

    public void DisplayInfo()
    {
        ScoreInfo.text = $"You survived {DateSystem.Instance.day} days";
        dayRecord = PlayerPrefs.GetInt("DayRecord", 0);
        if (DateSystem.Instance.day > dayRecord)
        {
            dayRecord = DateSystem.Instance.day;
            PlayerPrefs.SetInt("DayRecord", dayRecord);
        }
        RecordInfo.text = $"Your record is {dayRecord} days";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
