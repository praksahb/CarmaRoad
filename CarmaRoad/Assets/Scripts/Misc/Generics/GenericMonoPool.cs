using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMonoPool<T> : GenericMonoSingleton<GenericMonoPool<T>> where T : class
{
    private List<PooledItem<T>> pooledItems = new List<PooledItem<T>>();

    public virtual T GetItem()
    {
        if (pooledItems.Count > 0)
        {
            PooledItem<T> item = pooledItems.Find(i => i.IsUsed == false);
            if (item != null)
            {
                item.IsUsed = true;
                return item.Item;
            }
        }
        return CreateNewPooledItem();
    }

    public virtual void ReturnItem(T item)
    {
        PooledItem<T> pooledItem = pooledItems.Find(i => i.Item.Equals(item));
        if (pooledItem != null)
        {
            pooledItem.IsUsed = false;
        }

    }

    private T CreateNewPooledItem()
    {
        PooledItem<T> pooledItem = new PooledItem<T>();
        pooledItem.Item = CreateItem();
        pooledItem.IsUsed = true;
        pooledItems.Add(pooledItem);
        return pooledItem.Item;
    }

    protected virtual T CreateItem()
    {
        return null;
    }
}
public class PooledItem<T>
{
    public T Item;
    public bool IsUsed;
}
