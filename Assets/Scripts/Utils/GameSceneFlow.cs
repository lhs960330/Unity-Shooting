using System.Collections;
using System.Collections.Generic;
using Unity.Loading;
using Unity.VisualScripting;
using UnityEngine;

public class GameSceneFlow : MonoBehaviour
{
    [SerializeField] PooledObject bulletPrefab;
    [SerializeField] PooledObject effectPrefab;
    private void OnEnable()
    {
        Loading();
    }
    private void OnDisable()
    {
        UnLoading();
    }
    public void Loading()
    {
        Manager.pool.CreatePool("√—æÀ" , bulletPrefab, 20);
        Manager.pool.CreatePool("¿Ã∆Â∆Æ", effectPrefab, 10);
    }
    public void UnLoading()
    {
        // ¥ı¿ÃªÛ æ»æµ∂ß
        Manager.pool.RemovePool("√—æÀ");
        Manager.pool.RemovePool("¿Ã∆Â∆Æ");
    }

}
