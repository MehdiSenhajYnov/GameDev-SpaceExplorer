using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProgressionBar))]
public abstract class Building : MonoBehaviour, ClickableObject
{
    public static Color SelectedColor = new Color(0, 1, 0, 0.5f);
    public static Color ImpossibleColor = new Color(1, 0, 0, 0.5f);
    public static Color UnselectedColor = new Color(1, 1, 1, 0f);

    [SerializeField] List<GameObject> AllTiles;
    [SerializeField] public List<LevelInfo> LevelsInfo;
    [SerializeField] GameObject SelectionObj;

    
    private ProgressionBar progressionBar;
    private SpriteRenderer SelectionSpriteRenderer;
    protected SpriteRenderer GetSelectionSpriteRenderer
    {
        get
        {
            if (SelectionSpriteRenderer == null)
            {
                SelectionSpriteRenderer = SelectionObj.GetComponent<SpriteRenderer>();
            }
            return SelectionSpriteRenderer;
        }
    }

    public SpriteRenderer BuildSprite;
    
    public int currentLevel = -1;
    public int nextLevel => currentLevel + 1;
    private Collider2D[] cols;
    private bool isSelected = false;

    public void Start()
    {
        currentLevel = -1;
    }

    bool isBuilding => progressionBar != null && progressionBar.inProgress;

    public void OnSpriteClicked()
    {
        SetSelectedColor();

        if (isBuilding)
        {
            UpgradeButton.Instance.ActiveNonClickableButton();
            return;
        }
        if (nextLevel < LevelsInfo.Count)
        {
            BuyInfo.Instance.ShowBuyInfo(LevelsInfo[nextLevel].cost);
            if (LevelsInfo[nextLevel].cost.CanBuy())
            {
                UpgradeButton.Instance.ActiveButton(Upgrade);
            } else
            {
                UpgradeButton.Instance.ActiveNonClickableButton();
            }
        }
        else
        {
            UpgradeButton.Instance.ActiveNonClickableButton();
            BuyInfo.Instance.HideBuyInfo();
        }
    }

    void Upgrade()
    {
        if (nextLevel < LevelsInfo.Count)
        {
            if (!LevelsInfo[nextLevel].cost.CanBuy()) return;
            RessourceManager.Instance.Buy(LevelsInfo[nextLevel].cost);
            StartBuildBar();
            UpgradeButton.Instance.DeactiveButton();
        } else
        {
            currentLevel = LevelsInfo.Count - 1;
            UpgradeButton.Instance.ActiveNonClickableButton();
        }
    }

    public void OnOtherObjectClicked(GameObject otherClicked)
    {
        SetUnselectedColor();
        BuyInfo.Instance.HideBuyInfo();
        if (otherClicked != null && otherClicked == UpgradeButton.Instance.gameObject ) return;
        UpgradeButton.Instance.DeactiveButton();
    }

    public void OnSpriteClicking()
    {

    }
    public void OnSpriteUnClicked()
    {

    }
    public GameObject obj
    {
        get
        {
            return gameObject;
        }
    }

    public void MoveToMouse()
    {
        BuildMoving(MouseSelection.GetMouseWorldPositionRounded());
    }

    void BuildMoving(Vector3 MousePosition)
    {
        transform.position = MousePosition;
        foreach (var oneTile in AllTiles)
        {
            cols = Physics2D.OverlapPointAll(oneTile.transform.position);
            //Debug.Log("touching " + cols.Length);
            if (cols.Length <= 1)
            {
                SetSelectedColor();
            }
            else
            {
                SetCannotPlace();
                break;
            }
        }
    }

    public void StopMoving()
    {
        if (GetSelectionSpriteRenderer.color == ImpossibleColor || !LevelsInfo[0].cost.CanBuy())
        {
            Destroy(gameObject);
        } 
        else
        {
            RessourceManager.Instance.BuyBuild(this, LevelsInfo[0].cost);
            progressionBar = GetComponent<ProgressionBar>();
            StartBuildBar();
            SetUnselectedColor();

            DateSystem.Instance.OnDayChange += OnDayChangeBase;

        }
    }



    void StartBuildBar()
    {
        progressionBar.BuildTime = LevelsInfo[nextLevel].buildTime;
        progressionBar.StartProgressionBar(() =>
        {
            currentLevel++;

            if (currentLevel == 0)
            {
                OnBuild();
            }
            else
            {
                OnUpgrade();
            }

            if (isSelected && nextLevel < LevelsInfo.Count)
            {
                BuyInfo.Instance.ShowBuyInfo(LevelsInfo[nextLevel].cost);
                UpgradeButton.Instance.ActiveButton(Upgrade);
            }

            BuildSprite.sprite = LevelsInfo[currentLevel].sprite;
        });
    }
    

    void SetSelectedColor()
    {
        ChangeSelectionColor(SelectedColor);
        isSelected = true;
    }

    void SetUnselectedColor()
    {
        ChangeSelectionColor(UnselectedColor);
        isSelected = false;
    }

    void SetCannotPlace()
    {
        ChangeSelectionColor(ImpossibleColor);
    }

    void ChangeSelectionColor(Color selColor)
    {
        GetSelectionSpriteRenderer.color = selColor;
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
        if (DateSystem.Instance == null) return;
        DateSystem.Instance.OnDayChange -= OnDayChangeBase;
    }


    protected abstract void OnBuild();
    protected abstract void OnUpgrade();
    protected void OnDayChangeBase()
    {
        if (currentLevel < 0) return;
        OnDayChange();
    }

    protected abstract void OnDayChange();

}
