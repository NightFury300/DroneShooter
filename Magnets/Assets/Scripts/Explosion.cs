using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private float explosionRadius = 2.5f;

    void Start()
    {
        var eGo = GameObject.FindGameObjectsWithTag("Enemy");
        var pGo = GameObject.FindGameObjectWithTag("Player");
        if(pGo != null && IsInExplosionRadius(pGo)) GiveDamageandKnockback(pGo);
        foreach(GameObject _go in eGo)
        {
            if(_go != null && IsInExplosionRadius(_go))
                 GiveDamageandKnockback(_go);
        }
        Destroy(this.gameObject,gameObject.GetComponentInChildren<ParticleSystem>().main.duration);
    }

    bool IsInExplosionRadius(GameObject go)
    {
        if(Vector2.Distance(gameObject.transform.position,go.transform.position) <= explosionRadius)
            return true;
        return false;
    }

    void GiveDamageandKnockback(GameObject go)
    {
        if(go.tag.Equals("Player"))
            {
                go.GetComponent<Player>().TakeDamage(Enemy.maximumBlastDamage);
                go.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }

        if(go.tag.Equals("Enemy"))
        {
            go.GetComponent<Enemy>().TakeHealing(Bullet.bulletHealing);
            go.GetComponent<Enemy>().DisableTracking();
        }


        Vector2 knockbackForce = Enemy.maximumBlastKnockback * (go.transform.position - gameObject.transform.position);
        go.GetComponent<Rigidbody2D>().AddForce(knockbackForce,ForceMode2D.Impulse);
    }
}
