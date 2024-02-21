using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] PooledObject prefab;
    [SerializeField] int poolSize;

    private Stack<PooledObject> objectPool;

    public void CreatePool()
    {
        objectPool = new Stack<PooledObject>(poolSize);
        for(int i = 0; i < poolSize; i++)
        {            
            PooledObject instance = Instantiate(prefab);
            instance.gameObject.SetActive(false);
            instance.pooler = this;
            objectPool.Push(instance);

        }
    }

    public PooledObject GetPool()
    {
        if (objectPool.Count > 0)
        {
            PooledObject instance = objectPool.Pop();
            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            PooledObject instance = Instantiate(prefab);
            instance.pooler = this;
            return instance;
        }
    }

    public void ReturnPool(PooledObject instance)
    {
        if (objectPool.Count < poolSize)
        {
            instance.gameObject.SetActive(false);
            objectPool.Push(instance);
            return;
        }
        else
        {
            Destroy(instance.gameObject);
        }
        
    }
}
