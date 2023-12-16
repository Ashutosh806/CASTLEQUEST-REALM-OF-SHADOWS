using UnityEngine;

public class EventTrap : MonoBehaviour
{
    [SerializeField] private float damage = 1;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private LayerMask targetlayer; 
    
    public void Detection()
    {
        Vector2 trapPosition = transform.position;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(trapPosition, boxSize, 0f,targetlayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player")) 
            {
                
                PlayerLife playerHealth = collider.GetComponent<PlayerLife>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}