using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CameraMouvement : MonoBehaviour
{
    Camera _camera;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse2))
        {
            //transform.position -= new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
            // using transfor.Translate and Time.deltaTime for a smooth mouvement
            transform.Translate(-Input.GetAxis("Mouse X") * Time.deltaTime * speed, -Input.GetAxis("Mouse Y") * Time.deltaTime * speed, 0);
            CheckCameraPosition();
        }
        if (Input.GetKey(KeyCode.LeftControl) && _camera.orthographicSize != 0)
        {
            if (Input.mouseScrollDelta.y > 0 && _camera.orthographicSize > 2)
            {
                _camera.orthographicSize -= 1f;
            } else if (Input.mouseScrollDelta.y < 0 && _camera.orthographicSize < 27)
            {
                _camera.orthographicSize += 1f;
            }
            CheckCameraPosition();
        }



        /*
        with a size = 1,
        the camera at top left will be xMin = 1.3f and yMax = 98.5f
        the camera at bottom right will be xMax = 97.7f and yMin = 0.5f
        the camera at top right will be xMax = 97.7f and yMax = 98.5f
        the camera at bottom left will be xMin = 1.3f and yMin = 0.5f

        with a size = 5, 
        the camera at top left will be xMin = 8.5f and yMax = 94.5f
        the camera at bottom right will be xMax = 90.6f and yMin = 4.5f
        the camera at top right will be xMax = 90.6f and yMax = 94.5f
        the camera at bottom left will be xMin = 8.5f and yMin = 4.5f

        with a size = 10,
        the camera at top left will be xMin = 17.3f and yMax = 89.5f
        the camera at bottom right will be xMax = 81.7f and yMin = 9.5f
        the camera at top right will be xMax = 81.7f and yMax = 89.5f
        the camera at bottom left will be xMin = 17.3f and yMin = 9.5f

        with a size = 15,
        the camera at top left will be xMin = 26.2f and yMax = 84.5f
        the camera at bottom right will be xMax = 72.8f and yMin = 14.5f
        the camera at top right will be xMax = 72.8f and yMax = 84.5f
        the camera at bottom left will be xMin = 26.2f and yMin = 14.5f

        MaxSize = 27
        MinSize = 2

        Formule Approximative :

        xMin=1.3+(size−1)×1.8
        xMax=97.7−(size−1)×1.8
        yMin=0.5+(size/5)
        yMax=98.5−(size/5)
        */




        /* New Size and Min/Max
        Size 15,
        TopLeft : xMin = 11.5f, yMax = 93
        BottomRight : xMax = 87.5f, yMin = 6.5f

         */


    }

    public float GetXMin()
    {
        return 1.3f + 1.78f * (_camera.orthographicSize - 1);
    }
    public float GetXMax()
    {
        return 97.7f - 1.78f * (_camera.orthographicSize - 1);
    }

    public float GetYMin()
    {
        return 0.5f + _camera.orthographicSize;
    }
    public float GetYMax()
    {
        return 98.5f - _camera.orthographicSize;
    }

    void CheckCameraPosition()
    {
        if (transform.position.x < GetXMin())
        {
            transform.position = new Vector3(GetXMin(), transform.position.y, transform.position.z);
        }
        if (transform.position.x > GetXMax())
        {
            transform.position = new Vector3(GetXMax(), transform.position.y, transform.position.z);
        }
        if (transform.position.y < GetYMin())
        {
            transform.position = new Vector3(transform.position.x, GetYMin(), transform.position.z);
        }
        if (transform.position.y > GetYMax())
        {
            transform.position = new Vector3(transform.position.x, GetYMax(), transform.position.z);
        }
    }

}
