using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    private Animator anim;
    private bool isDead = false;
    [SerializeField] private AudioClip hurt,death;

    [Header("UI Settings")]
    public Image healthBarFill;

    [Header("Destroy Settings")]
    [SerializeField] private float levelDelay = 1f;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;

        UpdateHealthBar();
    }

    void Update()
    {
        if (currentHealth <= 0 && !isDead)
        {
            Debug.Log("Boss is dead");
            isDead = true;
            anim.SetBool("isDead", true);
            SoundManager.instance.PlaySound(death);
            StartCoroutine(LoadAfterDelay(levelDelay));
        }
    }
    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Max(currentHealth, 0f);
            UpdateHealthBar();
            anim.SetTrigger("Attacked");
            SoundManager.instance.PlaySound(hurt);
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            float fillAmount = currentHealth / maxHealth;
            healthBarFill.fillAmount = fillAmount;
        }
    }

    private IEnumerator LoadAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.instance.LoadNextLevel();  
    }
}
