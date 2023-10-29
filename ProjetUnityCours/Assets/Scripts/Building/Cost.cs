
[System.Serializable]
public class Cost
{
    public int CoinCost;
    public int WoodCost;

    public Cost(int coinCost, int woodCost)
    {
        CoinCost = coinCost;
        WoodCost = woodCost;
    }

    public bool CanBuy()
    {
        return RessourceManager.Instance.CanBuy(this);
    }
}