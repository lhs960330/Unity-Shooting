
using UnityEngine;


public class Gun : MonoBehaviour
{
    
    [SerializeField] Transform muzzlePoint;
    [SerializeField] int damage;
    [SerializeField] float maxDistance;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem hitEffect;

    // 어디 때리는 지 알기위해 만들어줌
    // [SerializeField] Transform hitPoint;
    public void Fire()
    {
        muzzleFlash.Play();
        // Physics 물리에 있는 Raycast를 사용하여 빛이 통과하지 못하는 물체를 만났을때를 정해준다.
        if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hitInfo, maxDistance))
        {
            Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance, Color.red, 0.3f);

            IDamageble damageble = hitInfo.collider.GetComponent<IDamageble>();
            damageble?.TakeDamage(damage);

            //원래는 맞는 쪽에 있어야되지만 시간상 여기다 다 때려 박음
            ParticleSystem effect = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            // 이펙트를 자식으로 만들어서 그 대상이 움직이거나 사라질때 같이 움직이거나 사라지게 해줄려고 이런 식으로 만들어준다.
            effect.transform.parent = hitInfo.transform;

            Rigidbody rigid = hitInfo.collider.GetComponent<Rigidbody>();
            // AddForceAtPosition이걸 하면 이 위치에서 힘을 가해주는 것
            if (rigid != null)
            {
                rigid.AddForceAtPosition(-hitInfo.normal * 10f, hitInfo.point, ForceMode.Impulse);
            }
            // 맞은 부분에 박히게 해줌
            //hitPoint.position = hitInfo.point;
        }
        else
        {
            Debug.Log("안맞음");
            Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance, Color.red, 0.3f);

        }
    }

}