using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, ClickableObject
{
    public static Color SelectedColor = new Color(0, 1, 0, 0.5f);
    public static Color ImpossibleColor = new Color(1, 0, 0, 0.5f);
    public static Color UnselectedColor = new Color(1, 1, 1, 0f);
    bool Clicked;
    [SerializeField] List<GameObject> AllTiles;
    [SerializeField] SpriteRenderer Texture;
    [SerializeField] Sprite[] LevelsSprites;

    [SerializeField] GameObject SelectionObj;
    SpriteRenderer SelectionSpriteRenderer;
    SpriteRenderer GetSelectionSpriteRenderer
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

    Collider2D[] cols;


    public int currentLevel = 0;
    public BuildingCost Cost = new BuildingCost(10, 5);

    public ProgressionBar progressionBar;
    public bool isSelected = false;

    public void OnSpriteClicked()
    {
        SetSelectedColor();
        if (progressionBar != null && progressionBar.inProgress)
        {
            UpgradeButton.Instance.ActiveNonClickable(Upgrade);
            return;
        }
        if (currentLevel < LevelsSprites.Length - 1)
            UpgradeButton.Instance.ActiveButton(Upgrade);
        else
            UpgradeButton.Instance.ActiveNonClickable(null);
    }


    void Upgrade()
    {
        if (currentLevel < LevelsSprites.Length)
        {
            progressionBar.runAtEnable = false;
            progressionBar.enabled = false;
            progressionBar.BuildTime = (int)(progressionBar.BuildTime * 1.5f);
            progressionBar.StartProgressionBar(() =>
            {
                currentLevel++;
                Texture.sprite = LevelsSprites[currentLevel];
            });
            UpgradeButton.Instance.ActiveNonClickable(Upgrade);
        } else
        {
            currentLevel = LevelsSprites.Length - 1;
            UpgradeButton.Instance.ActiveNonClickable(null);
        }
    }

    public void OnOtherObjectClicked(GameObject otherClicked)
    {
        SetUnselectedColor();
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
            Debug.Log("touching "+cols.Length);
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
        if (GetSelectionSpriteRenderer.color == ImpossibleColor)
        {
            Destroy(gameObject);
        } else
        {
            GameManager.Instance.BuyBuild(this);
            progressionBar = GetComponent<ProgressionBar>();
            progressionBar.runAtEnable = false;
            progressionBar.enabled = true;
            progressionBar.StartProgressionBar(() =>
            {
                if (isSelected)
                {
                    UpgradeButton.Instance.ActiveButton(Upgrade);
                }
            });

            SetUnselectedColor();
        }
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
}

public class BuildingCost
{
    public int CoinCost;
    public int WoodCost;

    public BuildingCost(int coinCost, int woodCost)
    {
        CoinCost = coinCost;
        WoodCost = woodCost;
    }
}