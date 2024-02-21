using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTest : MonoBehaviour
{
    [SerializeField] ObjectPooler pooler;

    private void Awake()
    {
       pooler.CreatePool();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PooledObject instance = pooler.GetPool();
            instance.transform.position = Random.insideUnitSphere * 10f;
        }      
    }
}
