using UnityEngine;
using System.Collections;
public class MouseControl : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 5f;

    [SerializeField]
    private float recoilForce = 5f;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform shootPoint;
    [SerializeField]
    private float shootCooldownTime = 2f;

    private bool canShoot = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        LookTowardsMouse();
        OnLeftClick();
    }

    private void LookTowardsMouse()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation,rotation,rotationSpeed*Time.deltaTime);
    }

    private void OnLeftClick()
    {
        Vector2 recoilDir = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 recoil = recoilDir.normalized;
        recoil *= recoilForce;

        
        if(Input.GetMouseButton(0) && canShoot)
        {
            rb.AddForce(recoil,ForceMode2D.Impulse);
            ShootBullet();
            StartCoroutine(StartCooldownTimer(shootCooldownTime));
        }
    }

    private void ShootBullet()
    {
        FindObjectOfType<AudioManager>().GetComponent<AudioManager>().Play("Shoot");
        Instantiate(bulletPrefab,shootPoint.position,shootPoint.rotation);
    }


    private IEnumerator StartCooldownTimer(float cooldownTimeInSeconds)
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldownTimeInSeconds);
        canShoot = true;
    }
}
