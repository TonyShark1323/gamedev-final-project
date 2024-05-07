using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public GameObject itemPrefab; // The prefab to pool, set this in the inspector
    private Queue<GameObject> items = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
        Initialize(3); // Initialize with a predefined number of items
    }

    private void Initialize(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newItem = Instantiate(itemPrefab);
            newItem.SetActive(false);
            items.Enqueue(newItem);
        }
    }

    public GameObject Get()
    {
        if (items.Count == 0)
            AddItems(1);  // Optionally expand the pool if empty
        var item = items.Dequeue();
        item.SetActive(true);
        return item;
    }

    public void ReturnToPool(GameObject item)
    {
        item.SetActive(false);
        items.Enqueue(item);
    }

    private void AddItems(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newItem = Instantiate(itemPrefab);
            newItem.SetActive(false);
            items.Enqueue(newItem);
        }
    }
}
