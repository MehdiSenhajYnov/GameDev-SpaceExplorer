using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public static List<GameObject> Backgrounds = new List<GameObject>();

    [SerializeField] Transform TopLeft;
    [SerializeField] Transform BottomRight;
    [SerializeField] GameObject Player;


    Vector3 TopLeftLimit, BottomLeftLimit, TopRightLimit, BottomRightLimit;

    [SerializeField] GameObject Background;
    [SerializeField] Transform SpawnPointRight, SpawnPointTop, SpawnPointLeft, SpawnPointBottom;
    [SerializeField] bool LeftExceeded, RightExceeded, TopExceeded, BottomExceeded;
    [SerializeField] bool LeftSpawned, RightSpawned, TopSpawned, BottomSpawned, TopRightSpawned, TopLeftSpawned, BottomRightSpawned, BottomLeftSpawned;

    private void Start()
    {
        TopLeftLimit = TopLeft.position;
        BottomLeftLimit = new Vector3(TopLeft.position.x, BottomRight.position.y, 0f);
        TopRightLimit = new Vector3(BottomRight.position.x, TopLeft.position.y, 0f);
        BottomRightLimit = BottomRight.position;

        Backgrounds.Add(gameObject);
    }

    private void Update()
    {

        if (Player.transform.position.x > TopRightLimit.x)
            RightExceeded = true;
        else
            RightExceeded = false;

        if (Player.transform.position.x < TopLeftLimit.x)
            LeftExceeded = true;
        else
            LeftExceeded = false;

        if (Player.transform.position.y > TopLeftLimit.y)
            TopExceeded = true;
        else
            TopExceeded = false;

        if (Player.transform.position.y < BottomLeftLimit.y)
            BottomExceeded = true;
        else
            BottomExceeded = false;

        if (RightExceeded)
        {
            if (!RightSpawned)
            {
                Instantiate(Background, SpawnPointRight.position, Quaternion.identity);
                Debug.Log("Right");

            }
            RightSpawned = true;
            if (TopExceeded && !TopRightSpawned)
            {
                Instantiate(Background, new Vector3(SpawnPointRight.position.x, SpawnPointTop.position.y, 0), Quaternion.identity);
                TopRightSpawned = true;
                Debug.Log("TopRight");
            }
            if (BottomExceeded && !BottomRightSpawned)
            {
                Instantiate(Background, new Vector3(SpawnPointRight.position.x, SpawnPointBottom.position.y, 0), Quaternion.identity);
                BottomRightSpawned = true;
                Debug.Log("BottomRight");
            }
        }
        if (LeftExceeded)
        {
            if (!LeftSpawned)
                Instantiate(Background, SpawnPointLeft.position, Quaternion.identity);
            LeftSpawned = true;
            Debug.Log("Left");
            if (TopExceeded && !TopLeftSpawned)
            {
                Instantiate(Background, new Vector3(SpawnPointLeft.position.x, SpawnPointTop.position.y, 0), Quaternion.identity);
                TopLeftSpawned = true;
                Debug.Log("TopLeft");
            }
            if (BottomExceeded && !BottomLeftSpawned)
            {
                Instantiate(Background, new Vector3(SpawnPointLeft.position.x, SpawnPointBottom.position.y, 0), Quaternion.identity);
                BottomLeftSpawned = true;
                Debug.Log("BottomLeft");
            }
        }
        if (TopExceeded && !TopSpawned)
        {
            Instantiate(Background, SpawnPointTop.position, Quaternion.identity);
            TopSpawned = true;
            Debug.Log("Top");
        }
        if (BottomExceeded && BottomSpawned)
        {
            Instantiate(Background, SpawnPointBottom.position, Quaternion.identity);
            BottomSpawned = true;
            Debug.Log("Bottom");
        }


        if (!LeftExceeded && !RightExceeded && !TopExceeded && !BottomExceeded)
        {
            int counter = 0;
            while (Backgrounds.Count > 1 && counter < Backgrounds.Count)
            {
                if (Backgrounds[counter] != gameObject)
                {
                    Destroy(Backgrounds[counter]);
                    Backgrounds.RemoveAt(counter);
                }
                else
                {
                    counter++;
                }
                Debug.Log("Destroying others backgrounds...");
            }
            LeftSpawned = false;
            RightSpawned = false;
            TopSpawned = false;
            BottomSpawned = false;
            TopRightSpawned = false;
            TopLeftSpawned = false;
            BottomRightSpawned = false;
            BottomLeftSpawned = false;

        }
            

    }
}
