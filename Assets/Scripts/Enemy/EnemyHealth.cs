using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    private float currentHealth;
    private Animator anim;
    private bool isDead = false;
    [SerializeField] private AudioClip hurt,death;

    [Header("Destroy Settings")]
    [SerializeField] private float destroyDelay = 2f;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = health;
    }

    void Update()
    {
        if (health < currentHealth)
        {
            currentHealth = health;
            anim.SetTrigger("Attacked");
            SoundManager.instance.PlaySound(hurt);
        }

        if (health <= 0 && !isDead)
        {
            Debug.Log("Enemy is dead");
            isDead = true;
            anim.SetBool("isDead", true);
            SoundManager.instance.PlaySound(death);

            if (GetComponentInParent<EnemyPatrol>() != null)
                GetComponentInParent<EnemyPatrol>().enabled = false;

            if (GetComponent<MeleeEnemy>() != null)
                GetComponent<MeleeEnemy>().enabled = false;

            StartCoroutine(DestroyAfterDelay(destroyDelay));
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(transform.parent.gameObject);
    }
}