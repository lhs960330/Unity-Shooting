using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPooler pooler;
    [SerializeField] bool autoRelease;
    [SerializeField] float releaseTime;

    public void OnEnable()
    {
        if (autoRelease)
        {
            releaseRoutine= StartCoroutine(ReleaseRoutine());
        }
    }
    public void OnDisable()
    {
        if(autoRelease)
        {
            StopCoroutine(releaseRoutine);
        }
    }

    Coroutine releaseRoutine;
    IEnumerator ReleaseRoutine()
    {
        yield return new WaitForSeconds(releaseTime);
        Release();
    }

    public void Release()
    {
        if (pooler != null)
        {
            pooler.ReturnPool(this);
        }
        else
        {
            // pooler 없으면 삭제
            Destroy(gameObject);
        }
    }
}
