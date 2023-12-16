using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    [SerializeField] private float healthAmount = 30f;
    [SerializeField] private float attackPowerIncrementOnCollect = 10.0f;
    [SerializeField] private AudioClip heal; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(heal);
            ApplyPowerUp(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void ApplyPowerUp(GameObject player)
    {
        if (player.TryGetComponent<PlayerLife>(out var playerLife))
        {
            playerLife.IncreaseHealth(healthAmount);
            GameManager gameManager = GameManager.instance;
            if (gameManager != null)
            {
                gameManager.IncrementAttackPower(attackPowerIncrementOnCollect);
            }
        }
    }
}