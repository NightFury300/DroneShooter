using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float healthDecrementInSeconds = 4f;
    [SerializeField]
    private float minBlastDetectionDistance = 2f;
    

    [SerializeField]
    private float setMaximumBlastDamage = 40f;
    [SerializeField]
    private float setMaximumBlastKnockback = 2f;
    [SerializeField]
    public static float maximumBlastDamage;
    [SerializeField]
    public static float maximumBlastKnockback;

    [SerializeField]
    private float velocity = 5f;

    [SerializeField]
    private bool isAlive = false;

    private Rigidbody2D rb;
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private bool isTracking = false;

    /*[SerializeField]
    private GameObject graphicsObject;
    [SerializeField]
    private float renderOffset;*/
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        isAlive = true;
        isTracking = true;
        maximumBlastDamage = setMaximumBlastDamage;
        maximumBlastKnockback = setMaximumBlastKnockback;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        {
            healthBar.SetHealthValue(currentHealth);
            //DisableGraphicsIfNotInView();
            DecreaseHealthOverTime();
            if(isTracking) MoveTowardsPlayer();
            if(CheckForPlayerProximity())
                TriggerExplosion();
        }
        if(GetComponent<Rigidbody2D>().velocity.magnitude <= 0.5f)
            isTracking = true;
        
    }

    void DecreaseHealthOverTime()
    {
        if(currentHealth > 0f)
            currentHealth -= healthDecrementInSeconds * Time.deltaTime;
        else
            {
                currentHealth = 0f;
                TriggerExplosion();
            }
    }

    void MoveTowardsPlayer()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 moveDirection = (playerPosition - new Vector2(transform.position.x,transform.position.y)).normalized;

        Vector2 movingPosition = moveDirection * velocity * Time.deltaTime;
        Vector2.ClampMagnitude(movingPosition,velocity);

        rb.MovePosition(rb.position + movingPosition);
    }

    bool CheckForPlayerProximity()
    {
        if(Vector2.Distance(player.transform.position,transform.position) <= minBlastDetectionDistance)
        {
            return true;
        }
        return false;
    }

    public void TakeHealing(float healing)
    {
        if(isAlive)
        {
            if(currentHealth + healing <=maxHealth)
                currentHealth += healing;
             else
                currentHealth = maxHealth;
        }
    }

    void TriggerExplosion()
    {
        isAlive = false;
        Instantiate(explosion,gameObject.transform.position,Quaternion.identity);

        PlayExplosionAudio();

        Die();
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    void PlayExplosionAudio()
    {
        float distance = Vector2.Distance(player.transform.position,gameObject.transform.position);
        float maxDistance = ((Camera.main.ViewportToWorldPoint(new Vector3(0.0f,0.0f,10.0f)) - Camera.main.ViewportToWorldPoint(new Vector3(1.0f,1.0f,10.0f))).magnitude)/2;
        float normalizedVol = 1.0f - (distance/maxDistance);

        if(!(normalizedVol >= 1))
            FindObjectOfType<AudioManager>().GetComponent<AudioManager>().Play("Explosion",normalizedVol);
        else return;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void DisableTracking()
    {
        isTracking = false;
    }

    /*void DisableGraphicsIfNotInView()
    {
        float minDistance = ((Camera.main.ViewportToWorldPoint(new Vector3(0.0f,0.0f,10.0f)) - Camera.main.ViewportToWorldPoint(new Vector3(1.0f,1.0f,10.0f))).magnitude)/2;

        if(Vector2.Distance(player.transform.position,gameObject.transform.position) < (minDistance + renderOffset))
            graphicsObject.SetActive(true);
        else
            graphicsObject.SetActive(false);
    }*/
}
