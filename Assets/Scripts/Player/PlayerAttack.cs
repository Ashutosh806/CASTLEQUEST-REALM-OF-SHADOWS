using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip attack,water;
    [SerializeField] private float attackPower = 10f;
    public float attackRate = 4f;
    private float nextAttackTime = 0f;

    public LayerMask enemy;
    
    public GameObject[] attackPoints;
    public float[] attackRadii;
    public int[] attackDamages;

    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
            {
                int attackType = 5;
                if (Input.GetKeyDown(KeyCode.J))
                    attackType = 1;
                else if (Input.GetKeyDown(KeyCode.K))
                    attackType = 2;
                else if (Input.GetKeyDown(KeyCode.L))
                    attackType = 3;

                animator.SetInteger("AttackType", attackType);
                nextAttackTime = Time.time + 1 / attackRate;
                
            }
            if (GameManager.instance.GetAttackPower() == GameManager.instance.GetMaxAttackPower() && Input.GetKeyDown(KeyCode.I))
            {
                animator.SetInteger("AttackType", 4);
                nextAttackTime = Time.time + 1 / attackRate;
                GameManager.instance.ResetAttackPower();
            }
        }
        else
        {
            animator.SetInteger("AttackType", 5);
        }
    }

    
    public void Attack(int attackType)
    {
        Debug.Log("Attack Type: " + attackType);
        if (attackType >= 1 && attackType <= attackPoints.Length)
        {
            float direction = transform.localScale.x;

            Vector3 offset = new Vector3(direction * 0f, 0, 0);
            Vector3 attackPosition = attackPoints[attackType - 1].transform.position + offset;

            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPosition, attackRadii[attackType - 1],enemy);

            foreach (Collider2D enemyCollider in enemies)
            {
             Debug.Log("Hit Enemy");
             if(enemyCollider.CompareTag("Boss"))
             {
                BossHealth bossHealth = enemyCollider.GetComponent<BossHealth>();
                if (bossHealth != null && bossHealth.maxHealth > 0)
                {
                 bossHealth.TakeDamage(attackDamages[attackType-1]);
                 GameManager.instance.IncrementAttackPower(attackPower);
                }
             }
             EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
             if (enemyHealth != null && enemyHealth.health > 0)
             {
                enemyHealth.health -= attackDamages[attackType - 1];
                GameManager.instance.IncrementAttackPower(attackPower);
             }
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < attackPoints.Length; i++)
        {
            Gizmos.color = Color.blue;
            float direction = transform.localScale.x;
            Vector3 offset = new(direction * 0f, 0, 0);
            Vector3 attackPosition = attackPoints[i].transform.position + offset;
            Gizmos.DrawWireSphere(attackPosition, attackRadii[i]);
        }
    }

    public void PlayAttackSound()
    {
       SoundManager.instance.PlaySound(attack);
    }

    public void PlayWaterSound()
    {
       SoundManager.instance.PlaySound(water);
    }
}