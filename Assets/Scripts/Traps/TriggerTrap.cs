using UnityEngine;

public class TriggerTrap : MonoBehaviour
{
    [SerializeField] private float damage = 1f;

    private void OnTriggerEnter2D(Collider2D other) 
    {
       if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerLife>().TakeDamage(damage);
        } 
    }
}