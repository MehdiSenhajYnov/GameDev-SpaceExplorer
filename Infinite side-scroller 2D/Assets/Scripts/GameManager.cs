using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_Text Timer;
    WaitForSeconds WaitOneSecond = new WaitForSeconds(1);
    [SerializeField]int StartTimerCount = 3;

    public TMP_Text Score;
    public static bool isGameStarted = false;

    public TMP_Text Loose;


    [SerializeField] Player playerMouvement;
    [SerializeField] PipeCreator pipeCreator;
    float timestart;

    // Game Settings
    public float YMin = -6.5f;
    public bool Dead = false;


    public float SpawnRatePipe;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Dead = false;
        //StartCoroutine(StartTimer(StartTimerCount));
        GameStart();
    }

    IEnumerator StartTimer(int timerCount)
    {
        Timer.gameObject.SetActive(true);
        int count = timerCount;
        while (count >= 0)
        {
            Timer.text = count.ToString();
            yield return WaitOneSecond;
            count--;
        }
        Timer.gameObject.SetActive(false);

        GameStart();
    }

    void GameStart()
    {
        timestart = Time.time;
        isGameStarted = true;
        playerMouvement.GameStart();
        pipeCreator.CreatePipe();
    }



    // Update is called once per frame
    void Update()
    {
        if (isGameStarted)
        {
            float time = Time.time - timestart;
            Score.text = "Score : " + time.ToString("0");

            if (playerMouvement.transform.position.y < YMin)
            {
                YouLose(true);
            }

            if (time != 0)
            {
                SpawnRatePipe = 4 / (time / 50);
            }
        }

        if (Dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void YouLose(bool DestroyPlayer)
    {
        Loose.gameObject.SetActive(true);
        isGameStarted = false;
        Dead = true;
        if (DestroyPlayer)
            Destroy(playerMouvement.gameObject);
    }
}
