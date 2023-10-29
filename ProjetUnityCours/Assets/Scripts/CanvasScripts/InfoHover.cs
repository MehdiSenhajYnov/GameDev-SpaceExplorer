using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoHover : MonoBehaviour, IPointerEnterHandler,  IPointerExitHandler
{

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.Play("InfoOpen");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.Play("InfoClose");
    }

}
