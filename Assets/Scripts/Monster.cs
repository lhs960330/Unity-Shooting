using UnityEngine;

public class Monster : MonoBehaviour, IDamageble
{
    [SerializeField] int hp;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] ParticleSystem hitEffect;
    public void TakeDamage(int damage)
    {
        hp -= damage;
        rigidbody.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        if (hp <= 0) Die();
    }
    private void Die()
    {
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);        
    }
}
