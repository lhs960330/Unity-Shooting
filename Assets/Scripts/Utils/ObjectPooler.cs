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
            // �ʹ� ������ ������ ����� �ڽ����� �ΰ����ش�.
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
            // Ǯ�������� �ڽĿ��� ���������ش�.
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
            // instance�� ������Ʈ���ƴ϶� ������Ʈ�� .gameObject�� ����� ������Ʈ�Ӹ��ƴ϶� ������Ʈ�� �ı��ȴ�.
            Destroy(instance.gameObject);
        }

    }
}
