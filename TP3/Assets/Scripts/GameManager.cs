using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    public GameObject WallPrefab;
    public GameObject FloorPrefab;
    public GameObject BoxPrefab;
    public GameObject GoalPrefab;
    public GameObject PlayerPrefab;

    public Transform WallParent;
    public Transform FloorParent;
    public Transform OtherParent;

    public SokobanGenerator sokobanGenerator;

    public Dictionary<TileType, GameObject> TileObjects;
    public Level level;
    public List<GoalCheck> Goals = new List<GoalCheck>();

    private GameObject player;
    public GameObject GetPlayer
    {
        get
        {
            if (player == null)
            {
                if (level.GetTilesOfType(TileType.Player).Count <= 0) return null;
                player = level.GetTilesOfType(TileType.Player)[0].gameObject;
            }
            return player;
        }
    }
    public Button UndoButton;
    public List<PlayerAction> PlayerActions = new List<PlayerAction>();

    public bool GamePaused;
    public TMP_Text stateText;
    public override void Awake()
    {
        base.Awake();
        TileObjects = new Dictionary<TileType, GameObject>() {
            {TileType.Wall, WallPrefab},
            {TileType.Floor, FloorPrefab},
            {TileType.Box, BoxPrefab},
            {TileType.Goal, GoalPrefab},
            {TileType.Player, PlayerPrefab},
        };
        DeactiveStateText();
    }



    // Start is called before the first frame update
    void Start()
    {
        level = sokobanGenerator.GenerateLevel();
        UndoButton.onClick.AddListener(Undo);
        UndoButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLevelSucced && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } else if (isLevelSucced && Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MenuScene");
        } else if (isLevelSucced && !GamePaused)
        {
            SetWin();
        } else if (GamePaused && Input.GetKeyDown(KeyCode.P))
        {
            DeactiveStateText();
        } else if (Input.GetKeyDown(KeyCode.P))
        {
            SetPause();
        }
    }
    public bool isLevelSucced
    {
        get
        {
            if (Goals.Count <= 0) return false;

            for (int i = 0; i < Goals.Count; i++)
            {
                if (Goals[i] && !Goals[i].onGoal)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public void SetPause()
    {
        stateText.text = "Pause";
        stateText.gameObject.SetActive(true);
        GamePaused = true;
    }

    public void SetWin()
    {
        stateText.text = "Win\nPress Espace to play again\nPress M to go back to menu";
        stateText.gameObject.SetActive(true);
        GamePaused = true;
    }

    public void DeactiveStateText()
    {
        stateText.gameObject.SetActive(false);
        GamePaused = false;
    }

    public void AddAction(PlayerAction playerAction)
    {
        if (PlayerActions.Count <= 10)
        {
            PlayerActions.Add(playerAction);
        } else
        {
            PlayerActions.RemoveAt(0);
            PlayerActions.Add(playerAction);
        }


        if (PlayerActions.Count > 0)
        {
            ActiveUndoButton();
        }
    }



    public void Undo()
    {

        PlayerAction playerAction = PlayerActions[PlayerActions.Count - 1];
        PlayerActions.RemoveAt(PlayerActions.Count - 1);
        Vector3 playerPosition = playerAction.playerposition;
        Vector3 cubePosition = playerAction.CubePosition;

        GetPlayer.transform.position = playerPosition;
        playerAction.Cube.transform.position = cubePosition;

        if (PlayerActions.Count <= 0)
        {
            DeactiveUndoButton();
        }
    }

    public void ActiveUndoButton()
    {
        UndoButton.interactable = true;
    }

    public void DeactiveUndoButton()
    {
        UndoButton.interactable = false;
    }
}
