using UnityEngine;

public class House : Building
{
    protected override void OnBuild()
    {
    }

    protected override void OnDayChange()
    {
        HouseLevelInfo houseLevelInfo;
        if (LevelsInfo[currentLevel] is HouseLevelInfo)
        {
            houseLevelInfo = (HouseLevelInfo)LevelsInfo[currentLevel];
            RessourceManager.Instance.AddRessource(houseLevelInfo.dailyGain);
        } else
        {
            Debug.LogWarning("HouseLevelInfo not found");
        }
    }

    protected override void OnUpgrade()
    {

    }
}
