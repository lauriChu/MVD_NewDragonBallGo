using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour {

    public static AudioSourcePool current;

    //Variables for sfx pooling

    public GameObject pooledObject;
    public int pooledAmount = 10;
    public bool willGrow = true;

    List<GameObject> pooledObjects;

    private void Awake()
    {
        current = this;
    }

    //Start: we create the SFX pool with all objects inactive
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }


    public GameObject GetPooledObject()
    {
        //Look for an available object in the pool
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        //If it's allowed to grow, let's add some more gameobjects to the pool
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            pooledObjects.Add(obj);
            return obj;
        }
        //If it isn't allowed to grow, return null
        return null;
    }

}
