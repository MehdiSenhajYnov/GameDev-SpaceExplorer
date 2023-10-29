using UnityEngine;
public class Bank : Building
{

    protected override void OnBuild()
    {
        BankLevelInfo bankLevelInfo;
        if (LevelsInfo[currentLevel] is BankLevelInfo)
        {
            bankLevelInfo = (BankLevelInfo)LevelsInfo[currentLevel];
            RessourceManager.Instance.ChangeMaxCoins(bankLevelInfo.maxCoins);
        } else
        {
            Debug.LogWarning("BankLevelInfo not found");
        }
    }

    protected override void OnDayChange()
    {
        if (currentLevel < 0)
        {
            return;
        }
    }

    protected override void OnUpgrade()
    {

    }
}
