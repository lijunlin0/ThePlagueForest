using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    private const int INIT_OBJ_COUNT = 100;
    private Dictionary<string, List<GameObject>> mGameobjects;
    
    public PoolManager()
    {
        mGameobjects=new Dictionary<string, List<GameObject>>();
    }

    private void AddGameObjects(string prefabName)
    {
        List<GameObject> list = mGameobjects[prefabName];
        GameObject prefab=Resources.Load<GameObject>("FightObject/"+prefabName);
        GameObject baseGameObject=GameObject.Instantiate(prefab);
        baseGameObject.name=prefabName;
        baseGameObject.SetActive(false);
        list.Add(baseGameObject);
         for (int i = 1; i < INIT_OBJ_COUNT; ++i)
        {
            GameObject gameObject=GameObject.Instantiate(baseGameObject);
            gameObject.name=prefabName;
            gameObject.SetActive(false);
            list.Add(gameObject);
        }
    }

    public GameObject GetGameObject(string prefabName)
    {
        if(!mGameobjects.ContainsKey(prefabName))
        {
            mGameobjects.Add(prefabName,new List<GameObject>());
        }
        List<GameObject> list = mGameobjects[prefabName];
        if(list.Count==0)
        {
            AddGameObjects(prefabName);
        }

        GameObject gameObject=list[list.Count-1];
        list.RemoveAt(list.Count-1);
        return gameObject;
    }
    public void PutGameObject(GameObject gameObject)
    {
        if (!mGameobjects.ContainsKey(gameObject.name))
        {
            mGameobjects.Add(gameObject.name, new List<GameObject>());
        }
        gameObject.SetActive(false);
        mGameobjects[gameObject.name].Add(gameObject);
    }
}