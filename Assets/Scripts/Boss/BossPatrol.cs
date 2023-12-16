using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatrol : MonoBehaviour
{
   
    [Header("Player")]
    [SerializeField] private Transform player;

   
    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    
    [Header("Movement")]
    [SerializeField] private float speed;
    private Vector3 initScale;  

    
    [SerializeField] private Animator anim;
    private bool isAttacking;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (!isAttacking)
        {
            int direction = (player.position.x > enemy.position.x) ? 1 : -1;
            MoveInDirection(direction);
        }
    }

    public void SetIsAttacking(bool attacking)
    {
        isAttacking = attacking;
    }

    private void MoveInDirection(int direction)
    {
        
        anim.SetBool("moving", true);

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
    }
}
