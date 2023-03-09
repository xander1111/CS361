using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private float _itemAppearTimeMin = 60f;
    private float _itemAppearTimeMax = 90f;
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
            Item[] itemOrder = items.OrderBy(_ => _random.Next()).ToArray(); 

            foreach (Item item in itemOrder)
            {
                float waitTime = Random.Range(_itemAppearTimeMin, _itemAppearTimeMax);
                yield return new WaitForSeconds(waitTime);
                _itemAppearTimeMin *= 2f;
                _itemAppearTimeMax *= 2f;
                
                Vector2 spawnLocation = new Vector2(Random.Range(GameManager.minVisibleX, GameManager.maxVisibleX), 1);

                Instantiate(item, spawnLocation, new Quaternion());
            }
        }
        // ReSharper disable once IteratorNeverReturns
    }
}
