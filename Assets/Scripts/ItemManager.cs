using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static float _itemAppearTimeMin = 30f;
    private static float _itemAppearTimeMax = 45f;
    private static System.Random _random;

    public List<Item> items;

    private void Start()
    {
        _random = new System.Random();
        StartCoroutine(SpawnItemLoop());
    }
    
    private IEnumerator SpawnItemLoop()
    {
        while (true)
        {
            // Use a random permutation of all available items to prevent items
            // from not appearing for an extended period of time
            Item[] itemOrder = items.ToArray();
            itemOrder = itemOrder.OrderBy(x => _random.Next()).ToArray();

            foreach (Item item in itemOrder)
            {
                float waitTime = Random.Range(_itemAppearTimeMin, _itemAppearTimeMax);
                yield return new WaitForSeconds(waitTime);

                Instantiate(item, new Vector3(0, Bed.maxY, 0), new Quaternion());
            }
        }
    }
}
