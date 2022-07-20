using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 2f;
    [SerializeField]
    private float bulletLifetimeInSeconds = 5f;

    [SerializeField]
    public static float bulletHealing = 20f;

    [SerializeField]
    private GameObject sparks;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
        Destroy(this.gameObject,bulletLifetimeInSeconds);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Instantiate(sparks,gameObject.transform.position,gameObject.transform.rotation);
        if(col.gameObject.tag == "Enemy")
            col.gameObject.GetComponent<Enemy>().TakeHealing(bulletHealing);
        Destroy(this.gameObject);  
    }
}
