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
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealthValue(currentHealth);
        IncreaseHealthOvertime();
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
        if(currentHealth - damage < 0.1f)
            currentHealth = 0.0f;
        else
            currentHealth -= damage;
        if(currentHealth <= 0.3f * maxHealth)
        {    
            playingLowHealthSound = true;
            PlayLowHealthSound();
        }
    }

    void PlayLowHealthSound()
    {
        FindObjectOfType<AudioManager>().GetComponent<AudioManager>().Play("LowHealth");
        FindObjectOfType<AudioManager>().GetComponent<AudioManager>().HighlightSound("LowHealth");

        FindObjectOfType<GameManager>().GetComponent<GameManager>().ActivateLowHealthVignette(true);
    }

    void StopLowHealthSound()
    {
        FindObjectOfType<AudioManager>().GetComponent<AudioManager>().Stop("LowHealth");
        FindObjectOfType<AudioManager>().GetComponent<AudioManager>().ResetSoundProperties("All");
        playingLowHealthSound = false;

        FindObjectOfType<GameManager>().GetComponent<GameManager>().ActivateLowHealthVignette(false); 
    }
}
