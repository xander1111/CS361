public class ItemExtraLife : Item
{
    protected override void Collect()
    {
        FindObjectOfType<GameplayUIManager>().AddLife();
    }
}
