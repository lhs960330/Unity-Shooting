using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPooler : MonoBehaviour
{
   
    private Stack<PooledObject> objectPool;
    private PooledObject prefab;
    private int size;

    public void CreatePool(PooledObject prefab, int size)
    {
        objectPool = new Stack<PooledObject>(size);
        this.prefab = prefab;
        this.size = size;
        for (int i = 0; i < size; i++)
        {
            PooledObject instance = Instantiate(prefab);
            instance.gameObject.SetActive(false);
            instance.pooler = this;
            // 너무 많으면 관리가 힘드니 자식으로 두게해준다.
            instance.transform.parent = transform;
            objectPool.Push(instance);

        }
    }

    public PooledObject GetPool(Vector3 position, Quaternion rotation)
    {
        if (objectPool.Count > 0)
        {
            PooledObject instance = objectPool.Pop();
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            // 풀었을때는 자식에서 나오게해준다.
            instance.transform.parent = null;
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
        if (objectPool.Count < size)
        {
            instance.gameObject.SetActive(false);
            instance.transform.parent = transform;
            objectPool.Push(instance);
            return;
        }
        else
        {
            // instance는 오브젝트가아니라 컴포넌트니 .gameObject를 써줘야 컴포넌트뿐만아니라 오브젝트를 파괴된다.
            Destroy(instance.gameObject);
        }

    }
}
