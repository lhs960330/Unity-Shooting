using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPooler pooler;

    public void OnEnable()
    {
        StartCoroutine(ReleaseRoutine());
    }

    IEnumerator ReleaseRoutine()
    {
        yield return new WaitForSeconds(2f);
        Release();
    }

    public void Release()
    {
        pooler.ReturnPool(this);
    }
}
