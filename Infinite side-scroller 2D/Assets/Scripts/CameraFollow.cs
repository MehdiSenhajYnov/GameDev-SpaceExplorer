using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public bool FollowX = true;
    public bool FollowY = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null) return;
        transform.position =
            new Vector3(
                        FollowX ? Target.position.x : transform.position.x,
                        FollowY ? Target.position.y : transform.position.y,
                        transform.position.z
                        );
    }
}
