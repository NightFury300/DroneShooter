using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool gameRunning = false;

    [Header("Mechanics")]

    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private float minDistance;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private float spawnRateInSeconds = 2.0f;
    [SerializeField]
    private GameObject lowHealthVignette;


    [Header("Borders")]
    [SerializeField]
    private Collider2D left;
    [SerializeField]
    private Collider2D right;
    [SerializeField]
    private Collider2D top;
    [SerializeField]
    private Collider2D bottom;

    private GameObject player;

    [Header("UI")]
    [SerializeField]
    private GameObject menuUI;
    [SerializeField]
    private GameObject runtimeUI;
    [SerializeField]
    private GameObject pauseUI;
    [SerializeField]
    private GameObject muteButton;
    [SerializeField]
    private GameObject unmuteButton;

    [SerializeField]
    private GameObject scoreUI;

    [SerializeField]
    private bool isAudioEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        minDistance = ((Camera.main.ViewportToWorldPoint(new Vector3(0.0f,0.0f,10.0f)) - Camera.main.ViewportToWorldPoint(new Vector3(1.0f,1.0f,10.0f))).magnitude)/2;
        maxDistance = minDistance * 2.5f;
        InvokeRepeating("SpawnEnemy",0.1f,spawnRateInSeconds);
        Score.Init(scoreUI,menuUI);
        Score.ResetScore();
        Score.UpdateHighScore(Score.GetCurrentHighScore());
    }

    public bool isGameRunning()
    {
        return gameRunning;
    }

    private void Update()
    {
        if(isGameRunning())
        {
            Score.IncreaseScore();          
        }
        if (Score.GetCurrentScore() > Score.GetCurrentHighScore())
        {
            Score.UpdateHighScore(Score.GetCurrentScore());
        }
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

    public void OnStartButtonClick()
    {
        gameRunning = true;
        MenuUIVisibility(false);
        RuntimeUIVisibility(true);
        ScoreUIVisibility(true);
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void OnMuteButtonClick()
    {
        isAudioEnabled = !isAudioEnabled;
        AudioListener.volume = isAudioEnabled? 1.0f:0.0f;
        muteButton.SetActive(isAudioEnabled);
        unmuteButton.SetActive(!isAudioEnabled);
    }

    public void OnPauseButtonClick()
    {
        gameRunning = false;
        RuntimeUIVisibility(false);
        PauseUIVisibility(true);
    }

    public void OnResumeButtonClick()
    {
        gameRunning = true;
        RuntimeUIVisibility(true);
        PauseUIVisibility(false);
    }

    public void OnBackToMenuButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void MenuUIVisibility(bool visible)
    {
        menuUI.SetActive(visible);
    }

    private void RuntimeUIVisibility(bool visible)
    {
        runtimeUI.SetActive(visible);
    }

    private void PauseUIVisibility(bool visible)
    {
        pauseUI.SetActive(visible);
    }

    private void ScoreUIVisibility(bool visible)
    {
        scoreUI.SetActive(visible);
    }
}
