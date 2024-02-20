using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] Gun[] guns;

    private Gun curGun;

    private void Start()
    {
        curGun = guns[0];
    }
    public void Fire()
    {
        curGun.Fire();
    }
}