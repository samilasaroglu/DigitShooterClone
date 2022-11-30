using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField] private GameData _gameData;
    [SerializeField] private GameObject[] prefab;
    [SerializeField] private int poolSize;
    private Queue<GameObject> pooledObjects;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        CreatePool();
    }

    public void CreatePool()
    {
        pooledObjects = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab[_gameData.BulletCount]);
            obj.transform.SetParent(transform);
            obj.SetActive(false);

            pooledObjects.Enqueue(obj);
        }
    }

    public GameObject GetDigitObject()
    {
        GameObject obj = pooledObjects.Dequeue();

        obj.SetActive(true);

        pooledObjects.Enqueue(obj);

        return obj;
    }
}
