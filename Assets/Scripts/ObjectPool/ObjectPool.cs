using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> poolItems;

    private void Start()
    {
        pooledObjects = new List<GameObject>();

        foreach (var item in poolItems)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy == false && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
