using UnityEngine;

public class ItemSpeedDown : Item {
    protected override void Collect()
    {
        Debug.Log("Collected");
        Monkey[] monkeys = FindObjectsOfType<Monkey>();
        foreach (Monkey monkey in monkeys) monkey.DecreaseSpeed();
    }
}
