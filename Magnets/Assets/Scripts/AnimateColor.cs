using UnityEngine;

public class AnimateColor : MonoBehaviour
{

    [SerializeField]
    private Gradient colorGradient;

    [SerializeField,Range(0.1f,10f)]
    private float animationSpeed;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private float currentTime = 0.0f;
    private bool  isIncreasing = true;

    // Update is called once per frame
    void Update()
    {
        AnimateColorWithTime();
    }

    void AnimateColorWithTime()
    {
        float normalizedTime = currentTime / animationSpeed;
        spriteRenderer.color = colorGradient.Evaluate(normalizedTime);
        if (currentTime >= animationSpeed)
            isIncreasing = false;

        else if (currentTime <= 0.0f)
            isIncreasing = true;

        if (isIncreasing) currentTime += Time.deltaTime;
        else currentTime -= Time.deltaTime;
    }
}
