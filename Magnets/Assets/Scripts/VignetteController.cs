using UnityEngine;
using UnityEngine.Rendering;

public class VignetteController : MonoBehaviour
{
    [SerializeField]
    private float vignetteTime;

    private float vignetteWeight = 0.0f;

    private bool fadingIn = true;
    private bool fadingOut = false;

    void OnEnable()
    {
        fadingIn = true;
        vignetteWeight = 0.0f;
    }

    void Update()
    {
        if(fadingIn)
            FadeIn();
        else if(fadingOut)
            FadeOut();
    }

    void FadeIn()
    {
        gameObject.GetComponent<Volume>().weight = vignetteWeight;
        vignetteWeight += 1/vignetteTime * Time.deltaTime;
        if(vignetteWeight >= 1.0f)
            {
                gameObject.GetComponent<Volume>().weight = 1.0f;
                fadingIn = false;
            }
    }

    void FadeOut()
    {
        gameObject.GetComponent<Volume>().weight = vignetteWeight;
        vignetteWeight -= 1/vignetteTime * Time.deltaTime;
        if(vignetteWeight <= 0.0f)
        {
            gameObject.GetComponent<Volume>().weight = 0.0f;
            fadingOut = false;
            gameObject.SetActive(false);
        }
    }

    public void Disable()
    {
        fadingOut = true;
    }
}
