
[System.Serializable]
public class Cost
{
    public int Coin;
    public int Wood;

    public Cost(int coinCost, int woodCost)
    {
        Coin = coinCost;
        Wood = woodCost;
    }

    public bool CanBuy()
    {
        return RessourceManager.Instance.CanBuy(this);
    }
}