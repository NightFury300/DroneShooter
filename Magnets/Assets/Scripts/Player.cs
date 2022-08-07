using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float healthIncrementInSeconds = 5.0f;
    [SerializeField]
    private bool playingLowHealthSound = false;
    [SerializeField]
    private bool playerAlive = true;

    private GameObject gameManager;
    private GameObject audioManager;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        gameManager = FindObjectOfType<GameManager>().gameObject;
        audioManager = FindObjectOfType<AudioManager>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAlive && gameManager.GetComponent<GameManager>().isGameRunning())
        {
            healthBar.SetHealthValue(currentHealth);
            IncreaseHealthOvertime();
        }
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    private void IncreaseHealthOvertime()
    {
        if(currentHealth < maxHealth)
            currentHealth += healthIncrementInSeconds * Time.deltaTime;
        else
            {
                currentHealth = maxHealth;
            }
        if(currentHealth >= 0.3f * maxHealth && playingLowHealthSound)
            StopLowHealthSound();
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth - damage < 0.1f)
        {
            currentHealth = 0.0f;
            PlayerDie();
        }
        else
            currentHealth -= damage;
        if(currentHealth <= 0.3f * maxHealth)
        {    
            playingLowHealthSound = true;
            PlayLowHealthSound();
        }
    }

    private void PlayerDie()
    {
        playerAlive = false;
        gameManager.GetComponent<GameManager>().TriggerGameOver();
    }

    void PlayLowHealthSound()
    {
        audioManager.GetComponent<AudioManager>().Play("LowHealth");
        audioManager.GetComponent<AudioManager>().HighlightSound("LowHealth");

        gameManager.GetComponent<GameManager>().ActivateLowHealthVignette(true);
    }

    void StopLowHealthSound()
    {
        audioManager.GetComponent<AudioManager>().Stop("LowHealth");
        audioManager.GetComponent<AudioManager>().ResetSoundProperties("All");
        playingLowHealthSound = false;

        gameManager.GetComponent<GameManager>().ActivateLowHealthVignette(false); 
    }
}
