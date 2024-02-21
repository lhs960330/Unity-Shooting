using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectTest : MonoBehaviour
{
    public PooledObject hitEffectPrefab;

    private void Start()
    {
        // Ư�� ���Ͽ��� �������� ���
        // �� ����� Resources���ϸ� �����ϴ�.
        hitEffectPrefab = Resources.Load<PooledObject>("HitEffect");
        Manager.pool.CreatePool("hit",hitEffectPrefab, 20);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Manager.pool.GetPool("hit", Random.insideUnitSphere * 10f, Quaternion.identity);

            //Manager.pool.GetPool("�Ѿ�", Random.insideUnitSphere * 10f, Quaternion.identity);

            // PooledObject instance = pooler.GetPool();
            // instance.transform.position = Random.insideUnitSphere * 10f;
        }
        /*if (Input.GetKeyDown(KeyCode.S))
        {
            //Manager.pool.GetPool("����Ʈ", Random.insideUnitSphere * 10f, Quaternion.identity);
        }*/
    }
}
