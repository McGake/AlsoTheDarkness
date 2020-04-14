using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class MyDictionaryEntry
    {
        public Levels key;
        public GameObject value;
    }

    [SerializeField]
    private List<MyDictionaryEntry> inspectorLevelsDictionary = new List<MyDictionaryEntry>();

    public Dictionary<Levels, GameObject> allLevels;

    private GameObject curLevel;

    private void Awake()
    {
        allLevels = new Dictionary<Levels, GameObject>();
        
        foreach (MyDictionaryEntry entry in inspectorLevelsDictionary)
        {
            allLevels.Add(entry.key, entry.value);
        }

        SetCurLevelIfNoneSet();

    }



    private void SetCurLevelIfNoneSet()
    {
        if (curLevel != null)
        {
            
        }
        else
        {
            foreach (KeyValuePair<Levels, GameObject> levelKV in allLevels)
            {
                if (levelKV.Value.activeInHierarchy)
                {
                    if (curLevel != null)
                    {
                        Debug.LogError("two scenes were active while transitioning. this should not be");
                    }
                    curLevel = levelKV.Value;
                }
            }
        }
    }

    public void LoadLevel(Levels level)
    {
        curLevel.SetActive(false);
        if (allLevels.ContainsKey(level))
        {
            curLevel = allLevels[level];
            curLevel.SetActive(true);
        }
        else
        {
            Debug.LogError("level key not found in dictionary of levels (level Manager)");
        }

    }


}
