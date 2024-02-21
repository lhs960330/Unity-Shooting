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
        Manager.pool.CreatePool("�Ѿ�" , bulletPrefab, 20);
        Manager.pool.CreatePool("����Ʈ", effectPrefab, 10);
    }
    public void UnLoading()
    {
        // ���̻� �Ⱦ���
        Manager.pool.RemovePool("�Ѿ�");
        Manager.pool.RemovePool("����Ʈ");
    }

}
