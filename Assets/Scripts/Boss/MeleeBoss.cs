using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBoss : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private float colliderDistance;
    [SerializeField] private PolygonCollider2D polyCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private AudioClip Attack;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    public PlayerLife playerLife;
    private BossPatrol bossPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        bossPatrol = GetComponentInParent<BossPatrol>();
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

        if (bossPatrol != null)
            bossPatrol.enabled = !PlayerInSight();
    }

    private void ChooseRandomAttack()
    {
        int randomAttack = Random.Range(1, 7);

        if (randomAttack == 1 || randomAttack == 2)
        {
            anim.SetTrigger("Attack1");
        }
        else if(randomAttack == 3 || randomAttack == 4)
        {
            anim.SetTrigger("Attack2");
        }
        else 
        {
            anim.SetTrigger("Attack3");
        }

    
    }

    public void StartAttackAnimation()
    {
        if (bossPatrol != null)
            bossPatrol.SetIsAttacking(true);
    }

    public void EndAttackAnimation()
    {
        if (bossPatrol != null)
            bossPatrol.SetIsAttacking(false);
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            polyCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(polyCollider.bounds.size.x * range, polyCollider.bounds.size.y, polyCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerLife = hit.transform.GetComponent<PlayerLife>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            polyCollider.bounds.center + colliderDistance * range * transform.localScale.x * transform.right,
            new Vector3(polyCollider.bounds.size.x * range, polyCollider.bounds.size.y, polyCollider.bounds.size.z));
    }

    public void DamagePlayer()
    {
        if (PlayerInSight())
            playerLife.TakeDamage(damage);
    }

    public void PlayAttackSound()
    {
        SoundManager.instance.PlaySound(Attack);
    }
}
