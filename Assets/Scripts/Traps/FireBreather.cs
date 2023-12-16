using UnityEngine;

public class FireBreather : MonoBehaviour
{
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float cooldownTime = 2f; 
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private int damage = 5;
    [SerializeField] private AudioClip fireExplosion;

    public Transform raycastOrigin; 

    private Animator animator;
    private bool canAttack = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
       
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, Vector2.left, detectionRange, playerLayer);
        if (hit.collider != null && canAttack)
        {
            Attack();
            Invoke(nameof(ResetCooldown), cooldownTime);
        }
    }

    private void Attack()
    {
        if (attackPoint != null)
        {
            animator.SetTrigger("Attack");
            canAttack = false;
        }
    }

    public void InflictDamage()
    {
        if (attackPoint != null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, radius, playerLayer);

            foreach (Collider2D player in colliders)
            {
                PlayerLife playerLife = player.GetComponent<PlayerLife>();
                if (playerLife != null)
                {
                    playerLife.TakeDamage(damage);
                }
            }
        }
    }

    public void PlaySound()
    {
        SoundManager.instance.PlaySound(fireExplosion);
    }

    private void ResetCooldown()
    {
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null && raycastOrigin != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(attackPoint.position, radius);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(raycastOrigin.position, Vector2.left * detectionRange);  
        }
    }
}