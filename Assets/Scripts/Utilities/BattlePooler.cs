using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class BattlePooler : MonoBehaviour
{
    public static BattlePooler pool;

    public Dictionary<GameObject, Pool> pools = new Dictionary<GameObject, Pool>();

    public List<Pool> viewableList = new List<Pool>();

    private Pool curPool;

    ObjectsInBattle objectsInBattle;

    public void Awake()
    {
        pool = this;
    }

    public void Start()
    {
        objectsInBattle = FindObjectOfType<ObjectsInBattle>();
        AddPoolToAbilities();
    }

    void AddPoolToAbilities() //TODO: this is temporary untill we totaly fix this jumble of a UI battle system and set up a proper event system to communicate with abilities
    {
        List<Ability> tempListToSet = new List<Ability>();
        foreach (GameObject pc in objectsInBattle.pcsInBattle)
        {
            pc.GetComponent<BattleActorView>().battlePooler = this;
            tempListToSet = pc.GetComponent<BattlePC>().abilities; //TODO: Decouple this

            foreach (Ability aB in tempListToSet)
            {
                aB.battlePooler = this;
            }
        }
        foreach (GameObject en in objectsInBattle.enemiesInBattle)
        {
            en.GetComponent<BattleActorView>().battlePooler = this;
            tempListToSet = en.GetComponent<BaseEnemy>().abilities; //TODO: Decouple this
            foreach (Ability aB in tempListToSet)
            {
                aB.battlePooler = this;
            }
        }
    }

    public GameObject ProduceObject(GameObject prefab)
    {
        viewableList.Clear();

        GameObject objectToProduce;

        if (pools.ContainsKey(prefab))
        {
            pools.TryGetValue(prefab, out curPool);

            for (int i = 0; i < pools.Count; i++)
            {
                viewableList.Add(pools.ElementAt(i).Value);
            }
            objectToProduce = curPool.GetNextPooledObject();
            objectToProduce.SetActive(true);
            return objectToProduce;
        }
        else
        {
            pools.Add(prefab, new Pool(prefab));
            pools.TryGetValue(prefab, out curPool);

            for (int i = 0; i < pools.Count; i++)
            {
                viewableList.Add(pools.ElementAt(i).Value);
            }
            return curPool.AddAdditionalObject();
        }


    }

    public GameObject ProduceObject(GameObject prefab, Vector3 position)
    {
        GameObject objectToProduce = ProduceObject(prefab);

        objectToProduce.transform.position = position;

        return objectToProduce;
    }

    public GameObject ProduceObject(GameObject prefab, Transform parent)
    {
        GameObject objectToProduce = ProduceObject(prefab);
        objectToProduce.transform.position = prefab.transform.position;
        objectToProduce.transform.SetParent(parent);

        return objectToProduce;
    }

    public GameObject ProduceObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject objectToProduce = ProduceObject(prefab);
        objectToProduce.transform.rotation = rotation;
        objectToProduce.transform.position = position;

        return objectToProduce;
    }

    public GameObject ProduceObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
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
