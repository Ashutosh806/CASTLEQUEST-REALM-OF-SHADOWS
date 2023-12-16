using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private AudioClip slash;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    public PlayerLife playerLife;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                ChooseRandomAttack();
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private void ChooseRandomAttack()
    {
        
        int randomAttack = Random.Range(1, 4); 

        if (randomAttack == 1)
        {
            anim.SetTrigger("Attack");
        }
        else
        {
            anim.SetTrigger("Attack2");
        }
    }

    public void StartAttackAnimation()
    {
        if (enemyPatrol != null)
            enemyPatrol.SetIsAttacking(true);
    }

    public void EndAttackAnimation()
    {
        if (enemyPatrol != null)
            enemyPatrol.SetIsAttacking(false);
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerLife = hit.transform.GetComponent<PlayerLife>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    public void DamagePlayer()
    {
        if (PlayerInSight())
            playerLife.TakeDamage(damage);
    }

    public void PlaySlashSound()
    {
        SoundManager.instance.PlaySound(slash);
    }
}
