public class ItemSpeedDown : Item
{
    protected override void Collect()
    {
        Monkey[] monkeys = FindObjectsOfType<Monkey>();
        foreach (Monkey monkey in monkeys) monkey.DecreaseSpeed();
    }
}
