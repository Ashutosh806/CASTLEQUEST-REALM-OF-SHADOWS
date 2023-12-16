using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Image playerHealthBar;
    public float playerMaxHealth = 100f;

    public Image powerBar;
    public float maxAttackPower = 100.0f;
    
    private float power = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdatePlayerHealthBar(float currentHealth)
    {
        float healthPercentage = currentHealth / playerMaxHealth;
        playerHealthBar.fillAmount = healthPercentage;
    }

    public void IncrementAttackPower(float attackPowerIncrement)
    {
        power += attackPowerIncrement;
        power = Mathf.Clamp(power, 0f, maxAttackPower);
        powerBar.fillAmount = power / maxAttackPower;
    }

    public float GetAttackPower()
    {
        return power;
    }

    public float GetMaxAttackPower()
    {
        return maxAttackPower;
    }

    public void ResetAttackPower()
    {
        power = 0f;
        powerBar.fillAmount = 0f;
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void ReloadLevel()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update() 
    {
        if(Input.GetKey(KeyCode.E))
        {
          LoadNextLevel();
        }
        if(Input.GetKey(KeyCode.Escape))
        {
          Application.Quit();
          Debug.Log("Quit");
        }
    }
}