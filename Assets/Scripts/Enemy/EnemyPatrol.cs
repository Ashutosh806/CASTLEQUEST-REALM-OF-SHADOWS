using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField]private float idleDuration;
    private float idleTimer;

    [SerializeField]private Animator anim;
    private bool isAttacking;

    private void Awake() 
    {
        initScale = enemy.localScale;
    }

    private void OnDisable() 
    {
        anim.SetBool("moving",false);    
    }

    private void Update() 
    {
       if(!isAttacking)
       {
        if(movingLeft)
        {
           if(enemy.position.x>=leftEdge.position.x)
            MoveInDirection(-1);
           else
           {
            DirectionChange();
           }       
        }
        else
        {
           if(enemy.position.x<=rightEdge.position.x)
            MoveInDirection(1);
           else
           {
            DirectionChange();
           }       
        }
       } 
    }

    public void SetIsAttacking(bool attacking)
    {
        isAttacking = attacking;
    }

    private void DirectionChange()
    {
        anim.SetBool("moving",false);

        idleTimer +=Time.deltaTime;

        if(idleTimer>idleDuration)
         movingLeft = !movingLeft;
    }
    
    private void MoveInDirection(int direction)
    {
        idleTimer = 0;
        anim.SetBool("moving",true);
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction,
        initScale.y,initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed,
        enemy.position.y,enemy.position.z);
    } 
}