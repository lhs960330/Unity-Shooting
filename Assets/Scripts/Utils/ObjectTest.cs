using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectTest : MonoBehaviour
{
    public PooledObject hitEffectPrefab;

    private void Start()
    {
        // 특정 파일에서 가져오는 방법
        // 이 방법은 Resources파일만 가능하다.
        hitEffectPrefab = Resources.Load<PooledObject>("HitEffect");
        Manager.pool.CreatePool("hit",hitEffectPrefab, 20);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Manager.pool.GetPool("hit", Random.insideUnitSphere * 10f, Quaternion.identity);

            //Manager.pool.GetPool("총알", Random.insideUnitSphere * 10f, Quaternion.identity);

            // PooledObject instance = pooler.GetPool();
            // instance.transform.position = Random.insideUnitSphere * 10f;
        }
        /*if (Input.GetKeyDown(KeyCode.S))
        {
            //Manager.pool.GetPool("이펙트", Random.insideUnitSphere * 10f, Quaternion.identity);
        }*/
    }
}
