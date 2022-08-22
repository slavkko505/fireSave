﻿using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

    public GameObject GetObject(GameObject gameObject, Transform transform)
    {
        if (objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
        {
            if (objectList.Count == 0)
            {
                return CreateNewObject(gameObject, transform);
            }
            else
            {
                GameObject _object = objectList.Dequeue();
                _object.SetActive(true);
                return _object;
            }
        }
        else
        {
            return CreateNewObject(gameObject, transform);
        }
    }

    private GameObject CreateNewObject(GameObject gameObject, Transform transform)
    {
        GameObject newGO = Instantiate(gameObject, transform);
        newGO.name = gameObject.name;
        return newGO;
    }

    public void ReturnGameObject(GameObject gameObject)
    {
        if (objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
        {
            objectList.Enqueue(gameObject);
        }
        else
        {
            Queue<GameObject> newObjectQueue = new Queue<GameObject>();
            newObjectQueue.Enqueue(gameObject);
            objectPool.Add(gameObject.name, newObjectQueue);
        }
        
        gameObject.SetActive(false);
    }
}
