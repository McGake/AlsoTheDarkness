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
        GameObject objectToProduce;

        if (pools.ContainsKey(prefab))
        {
            pools.TryGetValue(prefab, out curPool);
            objectToProduce = curPool.GetNextPooledObject();
            objectToProduce.SetActive(true);
            return objectToProduce;
        }
        else
        {
            pools.Add(prefab, new Pool(prefab));
            pools.TryGetValue(prefab, out curPool);
            return curPool.AddAdditionalObject();
        }
    }

    public static GameObject ProduceObject(GameObject prefab, Vector3 position)
    {
        GameObject objectToProduce = ProduceObject(prefab);
        objectToProduce.transform.position = position;

        return objectToProduce;
    }

    public static GameObject ProduceObject(GameObject prefab, Transform parent)
    {
        GameObject objectToProduce = ProduceObject(prefab);
        objectToProduce.transform.position = prefab.transform.position;
        objectToProduce.transform.SetParent(parent);

        return objectToProduce;
    }

    public static GameObject ProduceObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject objectToProduce = ProduceObject(prefab);
        objectToProduce.transform.rotation = rotation;
        objectToProduce.transform.position = position;

        return objectToProduce;
    }

    public static GameObject ProduceObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject objectToProduce = ProduceObject(prefab);
        objectToProduce.transform.position = position;
        objectToProduce.transform.rotation = rotation;
        objectToProduce.transform.SetParent(parent);

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

        if (pooledObjects[curIndex].activeSelf)
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
