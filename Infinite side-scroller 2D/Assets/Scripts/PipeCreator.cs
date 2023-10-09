using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PipeCreator : MonoBehaviour
{
    [SerializeField] Transform BottomTerrainHeighestPoint;
    [SerializeField] Transform TopTerrainLowestPoint;
    [SerializeField] GameObject PipePrefab;
    [SerializeField] float xSpawnPoint;
    [SerializeField] float yMinSpawnPoint;
    [SerializeField] float yMaxSpawnPoint;
    float DifficultyTimer = 2.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CreatePipe()
    {
        StartCoroutine(CreateBottomAndTopPipe());
    }
    public IEnumerator CreateBottomAndTopPipe()
    {
        while (GameManager.isGameStarted)
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
            float yspawnfirst = Random.Range(yMinSpawnPoint, yMaxSpawnPoint - 3.5f);
            GameObject PipeCreated = Instantiate(PipePrefab, new Vector3(xSpawnPoint, yspawnfirst, 0), Quaternion.identity);
            Transform BodyPipe = PipeCreated.transform.GetChild(0);
            BodyPipe.localScale = new Vector3(BodyPipe.localScale.x, (BodyPipe.transform.position.y - BottomTerrainHeighestPoint.position.y) / 2.5f, BodyPipe.localScale.z);
            Debug.Log("PipeCreated");
            float yspawnsecond = Random.Range(yspawnfirst + 2.5f, yMaxSpawnPoint);
            GameObject PipeCreated2 = Instantiate(PipePrefab, new Vector3(xSpawnPoint, yspawnsecond, 0), Quaternion.identity);
            PipeCreated2.transform.localScale = new Vector3(PipeCreated2.transform.localScale.x, -PipeCreated2.transform.localScale.y, PipeCreated2.transform.localScale.z);
            Transform BodyPipe2 = PipeCreated2.transform.GetChild(0);
            BodyPipe2.localScale = new Vector3(BodyPipe2.localScale.x, (TopTerrainLowestPoint.position.y - BodyPipe2.transform.position.y) / 2.5f, BodyPipe2.localScale.z);
            yield return new WaitForSeconds(DifficultyTimer);

        }
    }


    // pipe.y - maxPointTerrain.y / 3


    // Update is called once per frame
    void Update()
    {
        
    }
}
