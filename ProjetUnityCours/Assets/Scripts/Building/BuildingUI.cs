using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUI : MonoBehaviour, ClickableObject
{
    [SerializeField] GameObject buildingPrefab;
    [SerializeField] Transform buildingPrefabParent;
    GameObject createdObj;
    Building createdClickable;
    bool created;
    bool NoWorkerAvaible;
    public GameObject obj 
    { 
        get
        {
            return gameObject;
        }
    }

    public void OnOtherObjectClicked(GameObject otherClicked)
    {

    }
    Vector3 MouseStartPosition;
    public void OnSpriteClicked()
    {
        if (GameManager.Instance.IsWorkerAvaible() == false)
        {
            NoWorkerAvaible = true;
            return;
        } else
        {
            NoWorkerAvaible = false;
        }
        Debug.Log("BuildingUI clicked");
        created = false;
        MouseStartPosition = MouseSelection.GetMouseWorldPositionRounded();

    }



    public void OnSpriteClicking()
    {
        if (NoWorkerAvaible)
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

}
