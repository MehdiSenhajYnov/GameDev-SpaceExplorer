using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseSelection : MonoBehaviour
{
    private Collider2D[] cols;
    private Vector2 mousePos;
    private ClickableObject clickable;
    private bool clicking = false;

    static Camera mCamera;
    private void Start()
    {
        mCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = GetMouseWorldPosition();

            var UIClickedObj = DetectClickedUIObject();
            if (UIClickedObj != null)
            {
                HandleUIClick(UIClickedObj);
            }
            else
            {
                HandleWorldClick();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (clicking && clickable != null)
            {
                clickable.OnSpriteUnClicked();
            }
            clicking = false;
        }

        if (clicking && clickable != null)
        {
            clickable.OnSpriteClicking();
        }
    }

    GameObject DetectClickedUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (RaycastResult result in results)
        {
            return result.gameObject;
        }
        return null;
    }

    private void HandleUIClick(GameObject UIClickedObj)
    {
        ClickableObject temp = UIClickedObj.GetComponentInParent<ClickableObject>();
        if (clickable != null && clickable != temp)
        {
            if (temp != null)
            {
                clickable.OnOtherObjectClicked(temp.obj);
            } else
            {
                clickable.OnOtherObjectClicked(null);
            }
        }
        clickable = temp;
        if (clickable != null)
        {
            clicking = true;
            clickable.OnSpriteClicked();
        }
    }

    private void HandleWorldClick()
    {
        cols = Physics2D.OverlapPointAll(mousePos);
        
        float closestDistance = float.MaxValue;
        ClickableObject closestObject = null;

        foreach (Collider2D col in cols)
        {

            ClickableObject obj = col.GetComponentInParent<ClickableObject>();
            if (obj != null)
            {
                float distance = Vector2.Distance(mousePos, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestObject = obj;
                }
            }
        }

        if (closestObject != null)
        {
            clicking = true;
            if (clickable != null && closestObject != clickable)
            {
                clickable.OnOtherObjectClicked(closestObject.obj);
            }
            clickable = closestObject;
            clickable.OnSpriteClicked();
        } else if (clickable != null)
        {
            clickable.OnOtherObjectClicked(null);
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        return mCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    public static Vector3 GetMouseWorldPositionRounded()
    {

        var mousePosTemp = GetMouseWorldPosition();
        mousePosTemp.x = Mathf.Round(mousePosTemp.x);
        mousePosTemp.y = Mathf.Round(mousePosTemp.y);
        mousePosTemp.z = 0;
        return mousePosTemp;
    }
}
