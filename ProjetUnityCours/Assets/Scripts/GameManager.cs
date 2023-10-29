using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public const int canBuildLayer = 6;
    public const int cannotBuildLayer = 7;


    public List<Building> buildingsBuyed;


    #region Worker
    [SerializeField] TMPro.TMP_Text workerText;
    private int oldMaxWorker;
    private int maxWorker
    {
        get
        {
            int tempMaxWorker = 2 + (buildingsBuyed.Count / 10);
            if (oldMaxWorker != tempMaxWorker)
            {
                oldMaxWorker = tempMaxWorker;
                WorkerAvaible();
                UpdateWorkerText();
            }
            return tempMaxWorker;
        }
    }
    private int currentWorker;
    public int GetCurrentWorker { get { return currentWorker; } }
    public bool UseWorker()
    {
        if (currentWorker > 0)
        {
            currentWorker --;
            UpdateWorkerText();
            return true;
        }
        return false;
    }
    public void WorkerAvaible()
    {
        currentWorker++;
        if (currentWorker > maxWorker)
        {
            currentWorker = maxWorker;
        }
        UpdateWorkerText();
    }
    public void UpdateWorkerText()
    {
        workerText.text = $"{currentWorker}/{maxWorker}";
    }
    public bool IsWorkerAvaible()
    {
        return currentWorker > 0;
    }

    #endregion


    private void Start()
    {
        currentWorker = maxWorker;
        UpdateWorkerText();

    }

    public void GameOver()
    {
        DisplayLooseScreen.Instance.ShowLooseScreen();
    }



}
