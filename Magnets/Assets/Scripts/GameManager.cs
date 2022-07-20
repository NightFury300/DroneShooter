using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool gameRunning = false;

    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private float minDistance;
    [SerializeField]
    private float maxDistance;

    [SerializeField]
    private float spawnRateInSeconds = 2.0f;

    [SerializeField]
    private Collider2D left;
    [SerializeField]
    private Collider2D right;
    [SerializeField]
    private Collider2D top;
    [SerializeField]
    private Collider2D bottom;

    private GameObject player;

    [SerializeField]
    private GameObject lowHealthVignette;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        minDistance = ((Camera.main.ViewportToWorldPoint(new Vector3(0.0f,0.0f,10.0f)) - Camera.main.ViewportToWorldPoint(new Vector3(1.0f,1.0f,10.0f))).magnitude)/2;
        maxDistance = minDistance * 2.5f;
        InvokeRepeating("SpawnEnemy",0.1f,spawnRateInSeconds);
    }

    public bool isGameRunning()
    {
        return gameRunning;
    }

    void SpawnEnemy()
    {
        if (isGameRunning())
        {
            Vector2 spawnLoc = GenerateSpawnPoint();
            InstantiateEnemy(spawnLoc);
        }
    }

    Vector2 GenerateSpawnPoint()
    {
        Vector2 point;float distance;
        do
        {
            point = new Vector2(Random.Range(left.bounds.max.x,right.bounds.min.x),Random.Range(top.bounds.min.y,bottom.bounds.max.y));
            distance = Vector2.Distance(point,player.transform.position);
        } while(distance > maxDistance || distance < minDistance);
        return point;
    }

    void InstantiateEnemy(Vector2 pos)
    {
        Instantiate(enemy,pos,Quaternion.identity);
    }

    public void ActivateLowHealthVignette(bool active)
    {
        if(!active)
        {
            lowHealthVignette.gameObject.GetComponent<VignetteController>().Disable();
            return;
        }
        lowHealthVignette.SetActive(active);
    }
}
