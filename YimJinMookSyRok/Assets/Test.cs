using Script.Data;
using Script.Player;

public class Test : ItemData
{
    private void Awake()
    {
        name = ItemName.Test;
        action = GetItem;
    }

    protected override void GetItem(PlayerController data)
    {
        data.Stat.maxHealth += 10;
    }
}
