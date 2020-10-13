using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class BattlePooler
{
    public static Dictionary<GameObject, Pool> pools = new Dictionary<GameObject, Pool>();
    private static Pool curPool;

    public static GameObject ProduceObject(GameObject prefab)
    {
        GameObject objectToProduce = ProduceObjectUsingPooling(prefab);
        objectToProduce.SetActive(true);
        return objectToProduce;
    }

    private static GameObject ProduceObjectUsingPooling(GameObject prefab)
    {
        if (pools.ContainsKey(prefab) == false)
        {
            CreateNewPool(prefab);
        }
        SetCurPool(prefab);
        return ProduceObjectInCurPool();
    }
    private static void CreateNewPool(GameObject prefab)
    {
        pools.Add(prefab, new Pool(prefab));
    }
    private static void SetCurPool(GameObject prefab)
    {
        pools.TryGetValue(prefab, out curPool);
    }
    private static GameObject ProduceObjectInCurPool()
    {
        GameObject objectToProduce;
        objectToProduce = curPool.GetNextPooledObject();
        return objectToProduce;
    }



    public static GameObject ProduceObject(GameObject prefab, Vector3 position)
    {
        GameObject objectToProduce = ProduceObjectUsingPooling(prefab);
        objectToProduce.transform.position = position;
        objectToProduce.SetActive(true);
        return objectToProduce;
    }

    public static GameObject ProduceObject(GameObject prefab, Transform parent)
    {
        GameObject objectToProduce = ProduceObjectUsingPooling(prefab);
        objectToProduce.transform.position = prefab.transform.position;
        objectToProduce.transform.SetParent(parent,false);
        

        objectToProduce.SetActive(true);
        return objectToProduce;
    }

    public static GameObject ProduceObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject objectToProduce = ProduceObjectUsingPooling(prefab);
        objectToProduce.transform.rotation = rotation;
        objectToProduce.transform.position = position;
        objectToProduce.SetActive(true);
        return objectToProduce;
    }

    public static GameObject ProduceObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject objectToProduce = ProduceObjectUsingPooling(prefab);
        objectToProduce.transform.SetParent(parent);
        objectToProduce.transform.position = position;
        objectToProduce.transform.rotation = rotation;

        objectToProduce.SetActive(true);
        return objectToProduce;
    }
}

[System.Serializable]
public class Pool
{
    public List<GameObject> pooledObjects;
    public int curIndex;
    public GameObject prefab;

    public Pool(GameObject prefab)
    {
        pooledObjects = new List<GameObject>();
        curIndex = 0;
        this.prefab = prefab;
    }
    public GameObject GetNextPooledObject()
    {
        IncrementIndex();

        if(pooledObjects.Count == 0)
        {
            return AddAdditionalObject();
        }
        else if (pooledObjects[curIndex].activeSelf)
        {
            return AddAdditionalObject();
        }
        else
        {
            return pooledObjects[curIndex];
        }
        
    }
    public GameObject AddAdditionalObject()
    {
        GameObject objectToAdd = GameObject.Instantiate(prefab);
        pooledObjects.Add(objectToAdd);
        return objectToAdd;
    }
    private void IncrementIndex()
    {
        curIndex++;

        if(curIndex >= pooledObjects.Count)
        {
            curIndex = 0;
        }
    }
}
