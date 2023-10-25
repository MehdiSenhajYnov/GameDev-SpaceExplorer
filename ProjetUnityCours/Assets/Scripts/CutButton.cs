using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutButton : MonoBehaviour, ClickableObject
{
    public GameObject forestToCut;
    public ProgressionBar ProgressionBarPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    void CutTree()
    {
        if (forestToCut == null) return;
        if (!GameManager.Instance.IsWorkerAvaible()) return;
        var _progressionBar = Instantiate(ProgressionBarPrefab);
        _progressionBar.transform.position = forestToCut.transform.position;
        _progressionBar.transform.Translate(-0.4f, 0, 0);
        _progressionBar.BuildTime = 7;
        _progressionBar.enabled = true;
        int xPos = (int)forestToCut.transform.position.x;
        int yPos = (int)forestToCut.transform.position.y;
        GameObject forestTemp = forestToCut;
        _progressionBar.StartProgressionBar(() =>
        {
            Debug.Log("Cutting tree");
            TerrainGenerator.Instance.InstantiateTile(
                xPos, 
                yPos, 
                TerrainGenerator.Instance.TerrainTile, false);

            Destroy(forestTemp);
            RessourceManager.Instance.AddWood(1);
        });
    }

    public void ActiveButton(ForestClickable forest)
    {
        gameObject.SetActive(true);
        forestToCut = forest.gameObject;
    }

    public void DesactiveButton()
    {
        gameObject.SetActive(false);
        forestToCut = null;
    }

    public void OnSpriteClicked()
    {
        CutTree();
    }

    public void OnSpriteClicking()
    {
    }

    public void OnSpriteUnClicked()
    {
    }

    public void OnOtherObjectClicked(GameObject otherClicked)
    {
    }
    public GameObject obj
    {
        get
        {
            return gameObject;
        }
    }


}
