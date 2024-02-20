using UnityEngine;

public class Target : MonoBehaviour, IDamageble
{
    [SerializeField] int hp;
    [SerializeField] ParticleSystem hitEffect;
    private void Die()
    {
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
       
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Die();
        }
    }
}