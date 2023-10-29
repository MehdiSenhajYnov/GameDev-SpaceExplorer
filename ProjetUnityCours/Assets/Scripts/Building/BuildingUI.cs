using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour, ClickableObject
{
    public static Color CanBuyColor = new Color(1f, 1f, 1f);
    public static Color CannotBuyColor = new Color(0.25f, 0.25f, 0.25f);
    public Image image;
        
    [SerializeField] GameObject buildingPrefab;
    Cost buildingCost;
    [SerializeField] Transform buildingPrefabParent;
    GameObject createdObj;
    Building createdClickable;

    Transform SelectedTransform;
    public Transform GetSelectedTransform
    {
        get
        {
            if (SelectedTransform == null)
            {
                SelectedTransform = transform.GetChild(0);
            }
            return SelectedTransform;
        }
    }

    bool created;
    bool cannotBuild {
        get
        {
            return !GameManager.Instance.IsWorkerAvaible() || !buildingCost.CanBuy();
        }
    }

    public GameObject obj 
    { 
        get
        {
            return gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        buildingCost = buildingPrefab.GetComponent<Building>().LevelsInfo[0].cost;
        image = GetComponent<Image>();
    }

    Vector3 MouseStartPosition;
    public void OnSpriteClicked()
    {
        created = false;
        MouseStartPosition = MouseSelection.GetMouseWorldPositionRounded();

        BuyInfo.Instance.ShowBuyInfo(buildingCost);
        GetSelectedTransform.gameObject.SetActive(true);

    }



    public void OnSpriteClicking()
    {
        if (cannotBuild)
        {
            return;
        }
        if (!created && MouseSelection.GetMouseWorldPositionRounded() != MouseStartPosition)
        {
            created = true;
            createdObj = Instantiate(buildingPrefab, buildingPrefabParent);
            createdClickable = createdObj.GetComponent<Building>();
        }
        if (created && createdClickable != null)
        {
            createdClickable.MoveToMouse();
        }
    }

    public void OnSpriteUnClicked()
    {
        if (createdClickable != null)
        {
            createdClickable.StopMoving();
        }
        createdObj = null;
        createdClickable = null;
        created = false;
    }


    public void OnOtherObjectClicked(GameObject otherClicked)
    {
        BuyInfo.Instance.HideBuyInfo();
        GetSelectedTransform.gameObject.SetActive(false);
    }

    void Update()
    {
        if (buildingCost.CanBuy())
        {
            image.color = CanBuyColor;
        }
        else
        {
            image.color = CannotBuyColor;
        }
    }

}
