using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionFrame : Singleton<SelectionFrame>
{
    public void SetAt(Vector3 position)
    {
        transform.position = position;
    }
    public void SetAt(Vector3 position, float scale)
    {
        transform.position = position;
        transform.localScale = Vector3.one * scale;
    }

    public void Desactivate()
    {
        transform.position = Vector3.one * -1000;
    }

}
