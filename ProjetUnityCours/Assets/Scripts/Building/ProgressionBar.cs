using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionBar : MonoBehaviour
{
    // in game hours
    public int BuildTime = 10;
    public int currentBuildTime = 0;
    public bool inProgress = false;
    
    public Transform BuildBar;
    private Transform BuildBarFull;
    public Transform GetBuildBarFull
    {
        get
        {
            if (BuildBarFull == null)
            {
                BuildBarFull = BuildBar.GetChild(0);
            }
            return BuildBarFull;
        }
    }



    #region Constructors
    public ProgressionBar(int _buildTime, Transform _buildbar)
    {
        BuildBar = _buildbar;
        BuildTime = _buildTime;
    }

    public ProgressionBar(int _buildTime)
    {
        BuildTime = _buildTime;
    }
    public ProgressionBar()
    {
    }
    #endregion

    void ResetBar()
    {
        currentBuildTime = 0;
        GetBuildBarFull.localScale = new Vector3(0, GetBuildBarFull.localScale.y, GetBuildBarFull.localScale.z);

    }

    IEnumerator Progressing(bool haveToDisable)
    {
        ResetBar();
        inProgress = true;

        GameManager.Instance.UseWorker();
        BuildBar.gameObject.SetActive(true);
        int currentHour = DateSystem.Instance.hours;

        while (currentBuildTime < BuildTime)
        {
            yield return new WaitUntil(() => currentHour != DateSystem.Instance.hours);
            //yield return new WaitForSeconds(1);
            currentBuildTime++;
            GetBuildBarFull.localScale = new Vector3((float)currentBuildTime / (float)BuildTime, GetBuildBarFull.localScale.y, GetBuildBarFull.localScale.z);
            currentHour = DateSystem.Instance.hours;
        }
        GameManager.Instance.WorkerAvaible();
        inProgress = false;

        if (haveToDisable)
        {
            Desactive();
        }
    }

    IEnumerator Progressing(Action atEnd)
    {
        yield return StartCoroutine(Progressing(false));
        atEnd();
        Desactive();
    }

    public void StartProgressionBar()
    {
        StartCoroutine(Progressing(true));
    }

    public void StartProgressionBar(Action atEnd)
    {
        StartCoroutine(Progressing(atEnd));
    }


    void Desactive()
    {
        BuildBar.gameObject.SetActive(false);
        this.enabled = false;
    }
}
