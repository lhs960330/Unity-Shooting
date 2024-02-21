using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private Dictionary<string, ObjectPooler> poolDic = new Dictionary<string, ObjectPooler>();
       
    public void CreatePool(string name, PooledObject prefab, int size)
    {
        // 빈 오브젝트 생성
        GameObject poolObject = new GameObject($"Pool_{name}");
        // 매니저가 관리할 친구
        ObjectPooler pooler = poolObject.AddComponent<ObjectPooler>();
        pooler.CreatePool(prefab, size);

        poolDic.Add(name, pooler);
    } 

    public void RemovePool(string name)
    {
        ObjectPooler pooler = poolDic[name];
        // 또 실수함
        if (pooler != null)
            Destroy(pooler.gameObject);

        poolDic.Remove(name);
    }

    public PooledObject GetPool(string name, Vector3 position, Quaternion rotation)
    {
        return poolDic[name].GetPool(position, rotation);
    }
   
}
