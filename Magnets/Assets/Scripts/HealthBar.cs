using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Gradient healthGradient;

    private float maxHealth;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        SpriteRenderer[] renderer = GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer s in renderer)
        {
            if(s.gameObject.tag == "Fill")
                spriteRenderer = s;
        }
        if(GetComponentInParent<Enemy>())
            maxHealth = GetComponentInParent<Enemy>().GetMaxHealth();
        if(GetComponentInParent<Player>())
            maxHealth = GetComponentInParent<Player>().GetMaxHealth();
        SetHealthValue(maxHealth);
    }

    public void SetHealthValue(float health)
    {
        float normalizedHealth = health/maxHealth;

        spriteRenderer.color = healthGradient.Evaluate(normalizedHealth);

        Vector3 scale = spriteRenderer.gameObject.transform.localScale;
        scale.x = normalizedHealth;
        spriteRenderer.gameObject.transform.localScale = scale;
    }
}
