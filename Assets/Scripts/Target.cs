using UnityEngine;

public class Target : MonoBehaviour, IDamageble
{
    [SerializeField] int hp;
    
    private void Die()
    {
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