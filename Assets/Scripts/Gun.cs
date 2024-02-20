
using UnityEngine;


public class Gun : MonoBehaviour
{
    
    [SerializeField] Transform muzzlePoint;
    [SerializeField] int damage;
    [SerializeField] float maxDistance;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem hitEffect;

    // ��� ������ �� �˱����� �������
    // [SerializeField] Transform hitPoint;
    public void Fire()
    {
        muzzleFlash.Play();
        // Physics ������ �ִ� Raycast�� ����Ͽ� ���� ������� ���ϴ� ��ü�� ���������� �����ش�.
        if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hitInfo, maxDistance))
        {
            Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance, Color.red, 0.3f);

            IDamageble damageble = hitInfo.collider.GetComponent<IDamageble>();
            damageble?.TakeDamage(damage);

            //������ �´� �ʿ� �־�ߵ����� �ð��� ����� �� ���� ����
            ParticleSystem effect = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            // ����Ʈ�� �ڽ����� ���� �� ����� �����̰ų� ������� ���� �����̰ų� ������� ���ٷ��� �̷� ������ ������ش�.
            effect.transform.parent = hitInfo.transform;

            Rigidbody rigid = hitInfo.collider.GetComponent<Rigidbody>();
            // AddForceAtPosition�̰� �ϸ� �� ��ġ���� ���� �����ִ� ��
            if (rigid != null)
            {
                rigid.AddForceAtPosition(-hitInfo.normal * 10f, hitInfo.point, ForceMode.Impulse);
            }
            // ���� �κп� ������ ����
            //hitPoint.position = hitInfo.point;
        }
        else
        {
            Debug.Log("�ȸ���");
            Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance, Color.red, 0.3f);

        }
    }

}