using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private PlayerBlock playerBlock;
    public float maxHealth = 100f;
    private float currentHealth;
    private bool isDead = false;
    [SerializeField] private AudioClip hurt, death;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerBlock = GetComponent<PlayerBlock>();
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        if (playerBlock != null && playerBlock.IsInBlockState())
        {
            return;
        }

        anim.SetTrigger("hurt");
        SoundManager.instance.PlaySound(hurt);
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    void UpdateHealthBar()
    {
        GameManager.instance.UpdatePlayerHealthBar(currentHealth);
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
    }

    void Die()
    {
        anim.SetTrigger("death");
        SoundManager.instance.PlaySound(death);
        GameManager.instance.ReloadLevel();
    }  
}